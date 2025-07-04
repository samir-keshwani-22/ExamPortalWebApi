using Antlr4.Runtime;
using ExamPortal.Business.AntlrGrammar.Visitors;
using ExamPortal.Common.Exceptions;

namespace ExamPortal.Business.AntlrGrammar.Helpers
{
    public class VariableNameSemanticValidator
    {
        public static void ValidateVariableSemantics(List<VariableInfo> variables, string fieldName)
        {

        }

        public static List<VariableInfo> ExtractAndValidateVariables(string input, string fieldName)
        {
            var errorListener = new ErrorListener();
            var inputStream = new AntlrInputStream(input);
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

            ValidateVariableSemantics(visitor.Variables, fieldName);
            return visitor.Variables;
        }
    }
}