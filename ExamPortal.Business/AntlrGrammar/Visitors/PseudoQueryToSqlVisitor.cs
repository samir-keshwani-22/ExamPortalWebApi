using Antlr4.Runtime.Misc;
using ExamPortal.Business.AntlrGrammar.Helpers;

namespace ExamPortal.Business.AntlrGrammar.Visitors;

public class PseudoQueryToSqlVisitor : PseudoQueryExpressionBaseVisitor<string>
{
    private bool _hasPreExpression = false;
    private string _preExpressionType = "";

    public override string VisitAggregateQuery([NotNull] PseudoQueryExpressionParser.AggregateQueryContext context)
    {
        var aggregates = VisitAggregateList(context.aggregateList());

        if (context.whereClause()?.preExpression() != null)
        {
            _hasPreExpression = true;
            _preExpressionType = context.whereClause().preExpression().sourceDestSpecifier().GetText();
        }
        var whereClause = context.whereClause() != null ? VisitWhereClause(context.whereClause()) : "";
        var timeClause = context.timeFilter() != null ? VisitTimeFilter(context.timeFilter()) : "";

        string sql;

        if (_hasPreExpression && _preExpressionType == "{dest}")
        {

            sql = $@"SELECT {aggregates} 
        FROM cust_trans_info d
            JOIN transactions t
            ON d.trans_id = t.id
            AND d.trans_acct_type = 'Destination'
            JOIN cust_trans_info s
            ON s.trans_id = t.id
            AND s.trans_acct_type = 'Source'
        WHERE 
        COALESCE(t.env, 0) = %(env_var)d AND
        t.tenant_id = %(tenant_id)s AND (
        d.tenant_id = %(tenant_id)s AND d.acct_id = %(acct_id)s AND COALESCE(d.env, 0) = %(env_var)d AND COALESCE(s.env, 0) = %(env_var)d{whereClause}{timeClause}
        );";
        }
        else
        {
            sql = $@"SELECT {aggregates} 
        FROM 
            cust_trans_info a
            JOIN transactions t ON a.trans_id = t.id 
        WHERE 
            (COALESCE(t.env, 0) = %(env_var)d AND COALESCE(a.env, 0) = %(env_var)d) AND
            a.tenant_id = %(tenant_id)s AND
            t.tenant_id = %(tenant_id)s AND
            a.acct_id = %(acct_id)s{whereClause}{timeClause}
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
              return $"{Visit(agg)} AS {alias}";
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
        if (before)
        {
            var offset = context.timeReference().INT().GetText();
            if (_hasPreExpression && _preExpressionType == "{dest}")
            {
                return $@" AND
    d.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND
        d.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s)) AND 
        s.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND 
        s.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s)) AND
        t.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND 
        t.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s))";
            }
            else
            {
                return $@" AND
    a.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND
            a.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s)) AND 
            t.trans_date >= date_sub({unit}, {num}, date_sub({unit}, {offset}, toDateTime(%(trans_date)s))) AND 
            t.trans_date <= date_sub({unit}, {offset}, toDateTime(%(trans_date)s))";
            }
        }
        else
        {
            if (_hasPreExpression && _preExpressionType == "{dest}")
            {
                return $@" AND
    d.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND
        d.trans_date <= toDateTime(%(trans_date)s) AND 
        s.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND 
        s.trans_date <= toDateTime(%(trans_date)s) AND
        t.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND 
        t.trans_date <= toDateTime(%(trans_date)s)";
            }
            else
            {
                return $@" AND
    a.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND
            a.trans_date <= toDateTime(%(trans_date)s) AND 
            t.trans_date >= date_sub({unit}, {num}, toDateTime(%(trans_date)s)) AND 
            t.trans_date <= toDateTime(%(trans_date)s)";
            }
        }
    }

    public override string VisitWhereClause(PseudoQueryExpressionParser.WhereClauseContext context)
    {
        var expressionText = context.expression()?.GetText() ?? "";
        if (string.IsNullOrWhiteSpace(expressionText)) return "";

        string parsedCondition = PlaceholderMapper.ReplacePlaceholders(expressionText);

        // Don't add extra parentheses if there's a pre-expression
        if (_hasPreExpression)
        {
            return $" AND {parsedCondition}";
        }
        else
        {
            return $" AND (\n    {parsedCondition}";
        }
    }
}
