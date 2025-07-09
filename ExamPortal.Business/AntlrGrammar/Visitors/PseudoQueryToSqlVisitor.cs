// Updated PseudoQueryToSqlVisitor.cs with smarter dual join logic
using Antlr4.Runtime.Misc;
using ExamPortal.Business.AntlrGrammar.Helpers;

namespace ExamPortal.Business.AntlrGrammar.Visitors;

public class PseudoQueryToSqlVisitor : PseudoQueryExpressionBaseVisitor<string>
{
    private bool _hasPreExpression = false;
    private string _preExpressionRole = "";

    private bool RequiresDualJoin => _preExpressionRole == "{dest}" || _preExpressionRole == "{source}";

    public override string VisitAggregateQuery([NotNull] PseudoQueryExpressionParser.AggregateQueryContext context)
    {
        var aggregates = VisitAggregateList(context.aggregateList());

        if (context.whereClause()?.preExpression() != null)
        {
            var preText = context.whereClause().preExpression().GetText();

            // Only trigger dual-join if truly needed
            if (
                (preText.Contains("#{s.acct_id}") && preText.Contains("{d.related_parties}")) ||
                (preText.Contains("#{d.acct_id}") && preText.Contains("{s.related_parties}"))
            )
            {
                _hasPreExpression = true;
                _preExpressionRole = "{dest}";
            }
            else if (preText.Contains("#{account} is {source}"))
            {
                _preExpressionRole = "{source}";
            }
            else if (preText.Contains("#{account} is {dest}"))
            {
                _preExpressionRole = "{dest}";
            }
        }

        var whereClause = VisitWhereClause(context.whereClause(), context.timeFilter());

        string sql;

        if (RequiresDualJoin)
        {
            sql = $@"SELECT {aggregates} 
        FROM cust_trans_info d
            JOIN transactions t ON d.trans_id = t.id AND d.trans_acct_type = 'Destination'
            JOIN cust_trans_info s ON s.trans_id = t.id AND s.trans_acct_type = 'Source'
        WHERE 
            COALESCE(t.env, 0) = %(env_var)d AND
            t.tenant_id = %(tenant_id)s AND (
                {GetPrimaryAlias()}.tenant_id = %(tenant_id)s AND {GetPrimaryAlias()}.acct_id = %(acct_id)s AND
                COALESCE({GetPrimaryAlias()}.env, 0) = %(env_var)d AND COALESCE({GetSecondaryAlias()}.env, 0) = %(env_var)d
                {whereClause}
        );";
        }
        else
        {
            sql = $@"SELECT {aggregates} 
        FROM cust_trans_info a
            JOIN transactions t ON a.trans_id = t.id 
        WHERE 
            (COALESCE(t.env, 0) = %(env_var)d AND COALESCE(a.env, 0) = %(env_var)d) AND
            a.tenant_id = %(tenant_id)s AND
            t.tenant_id = %(tenant_id)s AND
            a.acct_id = %(acct_id)s{whereClause}
        );";
        }

        return sql;
    }

    public override string VisitAggregateList([NotNull] PseudoQueryExpressionParser.AggregateListContext context)
    {
        return string.Join(", ", context.aggregate().Select(agg =>
        {
            var func = agg.aggregateFunction().GetText();
            var alias = func.ToUpper() == "COUNT" ? "Count" : "Sum";
            return $"{Visit(agg)} as {alias}";
        }));
    }

    public override string VisitAggregate([NotNull] PseudoQueryExpressionParser.AggregateContext context)
    {
        var func = context.aggregateFunction().GetText();
        var arg = context.GetText().Contains("*") ? "*" : "t.amount";
        return $"{func}({arg})";
    }

    public override string VisitTimeFilter([NotNull] PseudoQueryExpressionParser.TimeFilterContext context)
    {
        var num = context.INT().GetText();
        var unit = context.TIMEUNIT().GetText();
        var before = context.timeReference().GetText().Contains("before");
        var offset = before ? context.timeReference().INT().GetText() : null;

        string prefix = RequiresDualJoin ? "d, s, t" : "a, t";
        var targets = prefix.Split(", ");

        string clause = string.Join(" AND\n        ", targets.Select(alias =>
        {
            if (before)
            {
                return $"{alias}.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND\n        {alias}.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s))";
            }
            else
            {
                return $"{alias}.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND\n        {alias}.trans_date <= toDateTime(%(trans_date)s)";
            }
        }));

        return clause;
    }

    public string VisitWhereClause(PseudoQueryExpressionParser.WhereClauseContext context, PseudoQueryExpressionParser.TimeFilterContext timeContext)
    {
        string preExpressionSql = "";
        if (context?.preExpression() != null)
        {
            var preExpressionText = context.preExpression().GetText().TrimEnd();
            if (preExpressionText.Contains("#{account} is {source}") || preExpressionText.Contains("#{account} is {dest}"))
            {
                preExpressionText = preExpressionText.Replace("#{account} is {source}", "");
                preExpressionText = preExpressionText.Replace("#{account} is {dest}", "");
                preExpressionText = preExpressionText.Trim();
                if (preExpressionText.EndsWith("AND", StringComparison.OrdinalIgnoreCase))
                    preExpressionText = preExpressionText.Substring(0, preExpressionText.Length - 3).TrimEnd();
            }

            preExpressionSql = PlaceholderMapper.ReplacePlaceholders(preExpressionText, GetPrimaryAlias());
        }

        var expressionText = context?.expression()?.GetText() ?? "";
        var mainExpressionSql = string.IsNullOrWhiteSpace(expressionText) ? "" : PlaceholderMapper.ReplacePlaceholders(expressionText, GetPrimaryAlias());

        var timeClause = timeContext != null ? VisitTimeFilter(timeContext) : "";

        var conditions = new[] { preExpressionSql, mainExpressionSql, timeClause }.Where(c => !string.IsNullOrWhiteSpace(c));
        if (!conditions.Any()) return "";

        var combined = string.Join(" AND ", conditions);
        return RequiresDualJoin ? $" AND {combined}" : $" AND (\n    {combined}\n)";
    }

    private string GetPrimaryAlias()
    {
        return _preExpressionRole == "{source}" ? "s" :
               _preExpressionRole == "{dest}" ? "d" : "a";
    }

    private string GetSecondaryAlias()
    {
        return _preExpressionRole == "{source}" ? "d" :
               _preExpressionRole == "{dest}" ? "s" : "a";
    }
}