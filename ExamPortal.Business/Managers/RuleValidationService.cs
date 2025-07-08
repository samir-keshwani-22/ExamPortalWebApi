using System.Text.RegularExpressions;
using Antlr4.Runtime;
using ExamPortal.API.Models;
using ExamPortal.Business.AntlrGrammar.Helpers;
using ExamPortal.Business.AntlrGrammar.Visitors;
using ExamPortal.Business.Interfaces;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Business.Managers;

/// <summary>
/// Provides the methods for validation of input request 
/// </summary>
public class RuleValidationService : IRuleValidationService
{
    #region Public Methods 

    /// <summary>
    /// Checks if the number of queries in the request exceeds the allowed threshold.
    /// </summary>
    /// <param name="request">The rule evaluator request.</param>
    /// <exception cref="RuleValidationException"></exception>
    public void CheckThreshold(RuleEvaluatorRequest request)
    {
        if (request.Queries != null && request.Queries.Count > 2)
        {
            throw new RuleValidationException("The queries array must have less than or equal to 2 items.");
        }
    }

    /// <summary>
    /// Validates the rules in specific request including queries, triggers, and result expressions.
    /// </summary>
    /// <param name="request">The rule evaluator request.</param>
    /// <exception cref="RuleValidationException"></exception>
    public void ValidateRules(RuleEvaluatorRequest request)
    {
        bool hasQueries = request.Queries != null && request.Queries.Any();
        bool hasTriggers = !string.IsNullOrWhiteSpace(request.Triggers);
        bool hasResult = !string.IsNullOrWhiteSpace(request.Result);

        if (!hasQueries && !hasTriggers)
        {
            throw new RuleValidationException($"At least one of queries or trigger needs to be present");
        }
        // either TT or FF 
        if (hasQueries != hasResult)
        {
            throw new RuleValidationException("Both 'queries' and 'result' must be provided together, or both must be absent.");
        }
        if (hasTriggers)
        {
            // bool triggerStartsWithQuey = TriggerStartsWithQuery(request.Triggers);
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



        var processor = new RuleProcessor();
        var sql = processor.GenerateSqlQueries(request);
        foreach (var asql in sql)
        {
            Console.WriteLine(asql);
        }

    }

    #endregion

    #region Private Validation Methods

    /// <summary>
    /// Validates the variable name using the VariableName grammar and semantic rules.
    /// </summary>
    /// <param name="variableName">The variable name to validate.</param>
    /// <param name="fieldName">The field name for context in error messages.</param>
    /// <exception cref="RuleValidationException"></exception>
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

    /// <summary>
    /// Validates the pseudo query using the PseudoQueryExpression grammar.
    /// </summary>
    /// <param name="input">The pseudo query string.</param>
    /// <param name="field">The field name for context in error messages.</param>
    /// <exception cref="RuleValidationException"></exception>
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

    /// <summary>
    /// Validates the triggers using the Triggers grammar.
    /// </summary>
    /// <param name="input">The triggers string.</param>
    /// <param name="field">The field name for context in error messages.</param>
    /// <exception cref="RuleValidationException"></exception>
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

    /// <summary>
    /// Validates the result expression using the PseudoResultExpression grammar.
    /// </summary>
    /// <param name="input">The result expression.</param>
    /// <param name="field">The field name for context in error messages.</param>
    /// <exception cref="RuleValidationException"></exception>
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

    #endregion

    #region Private Utility Methods 

    /// <summary>
    /// Ensures that all variables used in the result are declared in the queries.
    /// </summary>
    /// <param name="request">The rule evaluator request.</param>
    /// <exception cref="RuleValidationException"></exception>

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

    /// <summary>
    /// Retrieves identifiers used in the result expression.
    /// </summary>
    /// <param name="expr">The result expression.</param>
    /// <returns>A collection of identifier names.</returns>

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

    /// <summary>
    /// Retrieves declared variables from the grammar using the variable name.
    /// </summary>
    /// <param name="variableName">The variable name string.</param>
    /// <returns>A set of declared variable names.</returns>

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

    // private static bool TriggerStartsWithQuery(string trigger)
    // {
    //     var inputStream = new AntlrInputStream(trigger);
    //     var lexer = new TriggersLexer(inputStream);
    //     var tokenStream = new CommonTokenStream(lexer);
    //     tokenStream.Fill();
    //     var firstToken = tokenStream.GetTokens().FirstOrDefault(t => t.Channel == TokenConstants.DefaultChannel);

    //     return firstToken?.Type == TriggersParser.QUERY;
    // }

    #endregion

}
