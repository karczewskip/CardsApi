using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class AllowedForTypeRule(CardType[] requiredTypes) : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        return requiredTypes.Contains(cardDetails.CardType);
    }
}
