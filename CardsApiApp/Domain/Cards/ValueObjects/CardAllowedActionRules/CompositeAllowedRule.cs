using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class CompositeAllowedRule(ICardAllowedActionRule[] allRules) : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        foreach (var rule in allRules)
        {
            if (!rule.IsAllowed(cardDetails))
            {
                return false;
            }
        }
        return true;
    }
}
