using Antlr4.Runtime;
using ExamPortal.Business.AntlrGrammar.Visitors;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Business.AntlrGrammar.Helpers
{
    public class VariableNameSemanticValidator
    {
        public static void ValidateVariableSemantics(List<VariableInfo> variables, string fieldName)
        {
            if (variables == null || variables.Count == 0)
            {
                throw new RuleValidationException($"No variables found in {fieldName}");
            }
            if (variables.Count == 1) return;

            if (variables.Count == 2)
            {
                var var1 = variables[0];
                var var2 = variables[1];
                if (var1.Number != var2.Number)
                    throw new RuleValidationException($"Invalid variable combination in {fieldName}: Different N values - {var1.FullName} and {var2.FullName}. ");

                if (var1.AggregateType.Equals(var2.AggregateType))
                {
                    throw new RuleValidationException($"Invalid variable combination in {fieldName}: Same aggregate type - {var1.FullName} and {var2.FullName}. ");
                }
                return;
            }
            throw new RuleValidationException($"Invalid number of variables in {fieldName}: {variables.Count}");
        } 
    }
}