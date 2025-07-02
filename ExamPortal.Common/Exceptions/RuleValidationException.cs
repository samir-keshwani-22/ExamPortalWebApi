namespace ExamPortal.Common.Exceptions;

public class RuleValidationException : Exception
{
    public RuleValidationException(string message) : base(message) { }
}
