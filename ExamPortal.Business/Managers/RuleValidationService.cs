using System.Text.RegularExpressions;
using Antlr4.Runtime;
using ExamPortal.API.Models;
using ExamPortal.Business.AntlrGrammar.Helpers;
using ExamPortal.Business.AntlrGrammar.Visitors;
using ExamPortal.Business.Interfaces;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Business.Managers;

public class RuleValidationService : IRuleValidationService
{
    public void CheckThreshold(RuleEvaluatorRequest request)
    {
        if (request.Queries != null && request.Queries.Count > 2)
        {
            throw new RuleValidationException("The queries array must have less than or equal to 2 items.");
        }
    }

    public void ValidateRules(RuleEvaluatorRequest request)
    {
        bool hasQueries = request.Queries != null && request.Queries.Any();
        bool hasTriggers = !string.IsNullOrWhiteSpace(request.Triggers);
        bool hasResult = !string.IsNullOrWhiteSpace(request.Result);

        if (!hasQueries && !hasTriggers)
        {
            throw new RuleValidationException($"At least one of queries or trigger needs to be present");
        }
        if (hasQueries != hasResult)
        {
            throw new RuleValidationException("Both 'queries' and 'result' must be provided together, or both must be absent.");
        }
        if (hasTriggers)
        {
            bool triggerStartsWithQuey = request.Triggers.TrimStart().StartsWith("QUERY");

            if (!hasQueries && !triggerStartsWithQuey)
            {
                throw new RuleValidationException("When 'queries' is not present, 'triggers' must start with 'QUERY'.");
            }

            if (hasQueries && triggerStartsWithQuey)
            {
                throw new RuleValidationException("When 'queries' is present, 'triggers' must not start with 'QUERY'.");
            }

            // for validation of trigger 
            ValidateWithTriggerGrammar(request.Triggers, "triggers");
        }
        if (hasQueries)
        {
            foreach (var q in request.Queries!)
            {
                // for validation of variable name 
                ValidateVariableName(q.VariableName, $"queries.variable_name ({q.VariableName})");
                // for validation of pseudo_query validation 
                ValidateWithQueryGrammar(q.PseudoQuery, $"queries.pseudo_query ({q.VariableName})");
            }
        }

        // for validation of the result 
        if (hasResult)
        {
            ValidateWithResultGrammar(request.Result, "result");
            // for checking the unused variable 
            CheckVariablesInResultAreDeclared(request);
        }

    }

    private static void ValidateVariableName(string variableName, string fieldName)
    {
        try
        {
            var errorListener = new ErrorListener();
            var inputStream = new AntlrInputStream(variableName);
            var lexer = new VariableNameLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new VariableNameParser(tokenStream);

            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);


            var tree = parser.start();

            if (errorListener.HasErrors)
            {
                throw new RuleValidationException($"Invalid variable name syntax in {fieldName}: {errorListener.ErrorMessage}");
            }

            var visitor = new VariableNameCollectorVisitor();
            visitor.Visit(tree);

            VariableNameSemanticValidator.ValidateVariableSemantics(visitor.Variables, fieldName);
        }
        catch (RuleValidationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new RuleValidationException($"Error validating variable name in {fieldName}: {ex.Message}");
        }
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
        var declaredVariables = request.Queries.SelectMany(q => GetDeclaredVariablesFromGrammar(q.VariableName)).ToHashSet();
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

    private static HashSet<string> GetDeclaredVariablesFromGrammar(string variableName)
    {
        var inputStream = new AntlrInputStream(variableName);
        var lexer = new VariableNameLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new VariableNameParser(tokenStream);

        var tree = parser.start();

        var visitor = new VariableNameCollectorVisitor();
        visitor.Visit(tree);
        return visitor.Variables.Select(v => v.FullName).ToHashSet();
    }

}
