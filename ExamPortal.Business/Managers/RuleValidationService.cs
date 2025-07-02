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
        if (!string.IsNullOrWhiteSpace(request.Triggers))
        {
            ValidateWithQueryGrammar(request.Triggers, "triggers");
        }
        foreach (var q in request.Queries)
        {
            ValidateWithQueryGrammar(q.PseudoQuery, $"queries.pseudo_query ({q.VariableName})");
        }

        ValidateWithResultGrammar(request.Result, "result");

    }

    private void ValidateWithQueryGrammar(string input, string field)
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
    private void ValidateWithResultGrammar(string input, string field)
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


}
