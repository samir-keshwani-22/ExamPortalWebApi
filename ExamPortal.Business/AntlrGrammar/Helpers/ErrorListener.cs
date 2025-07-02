using Antlr4.Runtime;

namespace ExamPortal.Business.AntlrGrammar.Helpers;

public class ErrorListener : BaseErrorListener
{
    public bool HasErrors { get; private set; } = false;
    public string ErrorMessage { get; private set; } = "";

    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol,
        int line, int charPositionInLine, string msg, RecognitionException e)
    {
        HasErrors = true;
        ErrorMessage += $"line {line}:{charPositionInLine} {msg}\n";
    }
}
