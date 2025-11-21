using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class AllowedForAllCardsRule : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        return true;
    }
}
