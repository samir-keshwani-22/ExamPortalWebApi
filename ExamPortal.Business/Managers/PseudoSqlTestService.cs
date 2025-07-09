using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using ExamPortal.Business.AntlrGrammar.Visitors;

public class PseudoSqlTestResult
{
    public int Index { get; set; }
    public string PseudoQuery { get; set; }
    public string ExpectedSql { get; set; }
    public string GeneratedSql { get; set; }
    public bool Passed { get; set; }
}

public static class PseudoSqlTestService
{
    public static List<PseudoSqlTestResult> RunAllTests(string pseudoQueriesPath, string sqlsPath)
    {
        var pseudoQueries = File.ReadAllText(pseudoQueriesPath)
            .Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(q => q.Trim())
            .ToList();

        var sqlQueries = File.ReadAllText(sqlsPath)
            .Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(q => q.Trim())
            .ToList();

        var results = new List<PseudoSqlTestResult>();

        for (int i = 0; i < Math.Min(pseudoQueries.Count, sqlQueries.Count); i++)
        {
            var input = pseudoQueries[i];
            var expectedSql = sqlQueries[i];

            var inputStream = new AntlrInputStream(input);
            var lexer = new PseudoQueryExpressionLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new PseudoQueryExpressionParser(tokens);

            var tree = parser.query();
            var visitor = new PseudoQueryToSqlVisitor();
            var generatedSql = visitor.Visit(tree);

            bool passed = NormalizeSql(generatedSql) == NormalizeSql(expectedSql);

            results.Add(new PseudoSqlTestResult
            {
                Index = i + 1,
                PseudoQuery = input,
                ExpectedSql = expectedSql,
                GeneratedSql = generatedSql,
                Passed = passed
            });
        }

        return results;
    }

    private static string NormalizeSql(string sql)
    {
        return string.Concat(sql.Where(c => !char.IsWhiteSpace(c))).ToLowerInvariant();
    }
}