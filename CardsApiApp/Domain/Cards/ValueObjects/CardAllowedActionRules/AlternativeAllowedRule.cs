using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class AlternativeAllowedRule(ICardAllowedActionRule[] alternativeRules) : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        foreach (var rule in alternativeRules)
        {
            if (rule.IsAllowed(cardDetails))
            {
                return true;
            }
        }
        return false;
    }
}
