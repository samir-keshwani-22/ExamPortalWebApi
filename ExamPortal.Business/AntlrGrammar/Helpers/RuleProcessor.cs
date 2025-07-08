using Antlr4.Runtime;
using ExamPortal.API.Models;
using ExamPortal.Business.AntlrGrammar.Visitors;

namespace ExamPortal.Business.AntlrGrammar.Helpers;

public class RuleProcessor
{
    public List<string> GenerateSqlQueries(RuleEvaluatorRequest rule)
    {
        var queries = new List<string>();

        for (int i = 0; i < rule.Queries.Count; i++)
        {
            var query = rule.Queries[i]; 
            var input = query.PseudoQuery;

            var inputStream = new AntlrInputStream(input);
            var lexer = new PseudoQueryExpressionLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new PseudoQueryExpressionParser(tokens);

            var tree = parser.query();
            var visitor = new PseudoQueryToSqlVisitor( );
            var sql = visitor.Visit(tree);

            queries.Add(sql);
        }

        return queries;
    }
}
