using Antlr4.Runtime.Misc;

namespace ExamPortal.Business.AntlrGrammar.Visitors;

public class VariableInfo
{
    public int Number { get; set; }

    public required string AggregateType { get; set; }

    public string FullName => $"Q{Number}_{AggregateType}";
}
public class VariableNameCollectorVisitor : VariableNameBaseVisitor<object>
{
    public List<VariableInfo> Variables { get; } = new List<VariableInfo>();

    public override object VisitVariable([NotNull] VariableNameParser.VariableContext context)
    {
        var numberText = context.INT().GetText();
        var aggregateType = context.aggregateType().GetText();
        if (int.TryParse(numberText, out int number))
        {
            Variables.Add(
                new VariableInfo
                {
                    Number = number,
                    AggregateType = aggregateType
                }
            );
        }
        return base.VisitVariable(context);
    }

}
