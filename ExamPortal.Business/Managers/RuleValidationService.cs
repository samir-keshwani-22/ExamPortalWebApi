using System.Text.RegularExpressions;
using Antlr4.Runtime;
using ExamPortal.API.Models;
using ExamPortal.Business.AntlrGrammar.Helpers;
using ExamPortal.Business.Interfaces;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Business.Managers;

public class RuleValidationService : IRuleValidationService
{
    public void CheckThreshold(RuleEvaluatorRequest request)
    {
        if (request.Queries.Count > 2)
        {
            throw new RuleValidationException("The queries array must have less than or equal to 2 items.");
        }
    }

    public void ValidateRules(RuleEvaluatorRequest request)
    {
        bool hasQueries = request.Queries != null && request.Queries.Any();
        bool hasTriggers = !string.IsNullOrWhiteSpace(request.Triggers);

        if (!hasQueries && !hasTriggers)
        {
            throw new RuleValidationException($"At least one of queries or trigger needs to be present");
        }

        if (hasTriggers)
        {
            // for validation of trigger 
            ValidateWithTriggerGrammar(request.Triggers, "triggers");
        }
        if (hasQueries)
        {
            foreach (var q in request.Queries!)
            {
                // for validation of pseudo_query validation 
                ValidateWithQueryGrammar(q.PseudoQuery, $"queries.pseudo_query ({q.VariableName})");
            }
        }
        // for validation of the result 
        ValidateWithResultGrammar(request.Result, "result");
        // for checking the unused variable 
        CheckVariablesInResultAreDeclared(request);

    }

    private static void ValidateWithQueryGrammar(string input, string field)
    {
        var errorListener = new ErrorListener();
        var inputStream = new AntlrInputStream(input);
        var lexer = new PseudoQueryExpressionLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new PseudoQueryExpressionParser(tokenStream);

        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);

        parser.start();

        if (errorListener.HasErrors)
        {
            throw new RuleValidationException($"Invalid grammar in {field}: {errorListener.ErrorMessage}");
        }
    }
    private static void ValidateWithTriggerGrammar(string input, string field)
    {
        var errorListener = new ErrorListener();
        var inputStream = new AntlrInputStream(input);
        var lexer = new TriggersLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new TriggersParser(tokenStream);

        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);

        parser.start();

        if (errorListener.HasErrors)
        {
            throw new RuleValidationException($"Invalid grammar in {field}: {errorListener.ErrorMessage}");
        }
    }
    private static void ValidateWithResultGrammar(string input, string field)
    {
        var errorListener = new ErrorListener();
        var inputStream = new AntlrInputStream(input);
        var lexer = new PseudoResultExpressionLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new PseudoResultExpressionParser(tokenStream);

        parser.RemoveErrorListeners();
        parser.AddErrorListener(errorListener);

        parser.start();

        if (errorListener.HasErrors)
        {
            throw new RuleValidationException($"Invalid grammar in {field}: {errorListener.ErrorMessage}");
        }
    }

    private static void CheckVariablesInResultAreDeclared(RuleEvaluatorRequest request)
    {
        var declaredVariables = request.Queries
          .SelectMany(q => q.VariableName.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
          .Select(x => x.Trim())
          .ToHashSet();

        var usedVariables = GetIdentifiersFromResult(request.Result);

        var undeclared = usedVariables
            .Where(v => !declaredVariables.Contains(v))
            .ToList();

        if (undeclared.Any())
        {
            throw new RuleValidationException(
                $"Variables used in result but not declared in queries: {string.Join(", ", undeclared)}"
            );
        }
    }

    private static IEnumerable<string> GetIdentifiersFromResult(string expr)
    {
        var inputStream = new AntlrInputStream(expr);
        var lexer = new PseudoResultExpressionLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new PseudoResultExpressionParser(tokenStream);

        var tree = parser.start();

        var visitor = new ResultIdentifierCollectorVisitor();
        visitor.Visit(tree);

        return visitor.Identifiers;
    }

}
