using System.Collections.Generic;
using Antlr4.Runtime.Misc;

public class ResultIdentifierCollectorVisitor : PseudoResultExpressionBaseVisitor<object?>
{
    public HashSet<string> Identifiers { get; } = new HashSet<string>();

    public override object? VisitMath_term([NotNull] PseudoResultExpressionParser.Math_termContext context)
    {
        var identifierToken = context.IDENTIFIER();
        if (identifierToken != null)
            Identifiers.Add(identifierToken.GetText());
        return base.VisitMath_term(context);
    } 
}