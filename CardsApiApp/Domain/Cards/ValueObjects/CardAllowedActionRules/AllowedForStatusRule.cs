using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class AllowedForStatusRule(CardStatus[] requiredStatuses) : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        return requiredStatuses.Contains(cardDetails.CardStatus);
    }
}
