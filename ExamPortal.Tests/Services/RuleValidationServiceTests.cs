using ExamPortal.API.Models;
using ExamPortal.Business.Managers;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Tests.Services;

public class RuleValidationServiceTests
{
    private readonly RuleValidationService _service;

    public RuleValidationServiceTests()
    {
        _service = new();
    }

    [Fact]
    public void CheckThreshold_Throws_WhenMoreThanTwoQueries()
    {
        var request = new RuleEvaluatorRequest()
        {
            Queries = new List<Query>
            {
                new(),new () ,new ()
            }
        };

        Assert.Throws<RuleValidationException>(() => _service.CheckThreshold(request));
    }

    [Fact]
    public void CheckThreshold_DoesNotThrows_WhenTwoOrFewerQueries()
    {
        var request = new RuleEvaluatorRequest()
        {
            Queries = new List<Query>
            {
                new(),new ()
            }
        };

        var exception = Record.Exception(() => _service.CheckThreshold(request));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateRules_Throws_WhenBothQueriesAndTriggersAreEmpty()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "",

        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Equal("At least one of queries or trigger needs to be present", exception.Message);
    }

    [Fact]
    public void ValidateRules_Throws_WhenTriggersHaveInvalidGrammar()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "Q1_sum > 0 AND Q1_count >= 3 AND (Q1_count/Q1_sum)*100 >= 150 AND (Q1_count/Q1_sum)*100 >= 150",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERY COUNT({amount}), SUM({amount})\nFROM DATA\nPAST 7 day FROM transaction date\nWHERE #{amount} >= {5000}", VariableName = "Q1_count,Q1_sum" }
            },
            Triggers = "invalid grammar"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Contains("Invalid grammar in", exception.Message);
    }

    [Fact]
    public void ValidateRules_Throw_WhenTriggesMustStartWithQueryButDoesNot()
    {
        var request = new RuleEvaluatorRequest()
        {
            Triggers = "#{amount} >= {5000}"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Equal("When 'queries' is not present, 'triggers' must start with 'QUERY'.", exception.Message);
    }


    [Fact]
    public void ValidateRules_Throws_WhenTriggersMustNotStartWithQueryButDoes()
    {
        var request = new RuleEvaluatorRequest()
        {
            Triggers = "QUERY COUNT({amount}) FROM DATA WHERE #{amount} >= {5000}",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERY COUNT({amount}) FROM DATA WHERE #{amount} >= {5000}", VariableName = "Q1_count" }
            },
            Result = "Q1_count > 0"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Equal("When 'queries' is present, 'triggers' must not start with 'QUERY'.", exception.Message);
    }

    [Fact]
    public void ValidateRules_DoesNotThrow_WhenTriggersStartWithQueryAndNoQueries()
    {
        var request = new RuleEvaluatorRequest()
        {
            Triggers = "QUERY COUNT(*) FROM DATA WHERE #{account} is {dest} AND #{amount} >= {5000}"

        };

        var exception = Record.Exception(() => _service.ValidateRules(request));
        Assert.Null(exception);
    }


    [Fact]
    public void ValidateRules_DoesNotThrow_WhenTriggersNotStartWithQueryAndQueriesPresent()
    {
        var request = new RuleEvaluatorRequest()
        {
            Triggers = "#{amount} >= {5000}",
            Queries = new List<Query>
        {
            new() {PseudoQuery = "QUERY COUNT({amount}) FROM DATA WHERE #{amount} >= {5000}", VariableName = "Q1_count" }
        },
            Result = "Q1_count > 0"
        };

        var exception = Record.Exception(() => _service.ValidateRules(request));
        Assert.Null(exception);
    }


    [Fact]
    public void ValidateRules_Throws_WhenQueriesHaveInvalidGrammar()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "Q1_count > 0 AND Q1_count >= 3 AND (Q1_count/Q1_sum)*100 >= 150 AND (Q1_sum/Q1_count)*100 >= 150",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERYbogus COUNT({amount}), SUM({amount})\nFROM DATA\nPAST 7 day FROM transaction date\nWHERE #{amount} >= {5000}", VariableName = "Q1_count ,Q1_sum" }
            },
            Triggers = "#{amount} >= {5000}"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Contains("Invalid grammar in queries.pseudo_query (Q1_count ,Q1_sum)", exception.Message);
    }

    [Fact]
    public void ValidateRules_Throws_WhenResultHasInvalidGrammar()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "bogusQ2_count> 0 AND Q1_sum >= 3 AND (Q1_count/Q1_sum)*100 >= 150 AND (Q1_count/Q1_sum)*100 >= 150",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERY COUNT({amount}), SUM({amount})\nFROM DATA\nPAST 7 day FROM transaction date\nWHERE #{amount} >= {5000}", VariableName = "Q1_count, Q1_sum" }
            },
            Triggers = "#{amount} >= {5000}"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Contains("Invalid grammar in result", exception.Message);

    }

    [Fact]
    public void ValidateRules_Throws_WhenResultUsesUndeclaredVariables()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "Q5_count > 0 AND Q6_count >= 3 AND (Q1_count/Q2_count)*100 >= 150 AND (Q1_sum/Q1_sum)*100 >= 150",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERY COUNT({amount}), SUM({amount})\nFROM DATA\nPAST 7 day FROM transaction date\nWHERE #{amount} >= {5000}", VariableName = "Q1_count,Q1_sum" }
            },
            Triggers = "#{amount} >= {5000}"
        };

        var exception = Assert.Throws<RuleValidationException>(() => _service.ValidateRules(request));
        Assert.Contains("Variables used in result but not declared in queries: Q5_count, Q6_count", exception.Message);
    }

    [Fact]
    public void ValidateRules_DoesNotThrows_WhenAllVariablesAreDeclared()
    {
        var request = new RuleEvaluatorRequest()
        {
            Result = "Q1_sum > 0 AND Q1_count >= 3 AND (Q1_count/Q1_sum)*100 >= 150 AND (Q1_count/Q1_sum)*100 >= 150",
            Queries = new List<Query>
            {
                new() {PseudoQuery = "QUERY COUNT({amount}), SUM({amount})\nFROM DATA\nPAST 7 day FROM transaction date\nWHERE #{amount} >= {5000}", VariableName = "Q1_count,Q1_sum" }
            },
            Triggers = "#{amount} >= {5000}"
        };

        var exception = Record.Exception(() => _service.ValidateRules(request));
        Assert.Null(exception);
    }

}
