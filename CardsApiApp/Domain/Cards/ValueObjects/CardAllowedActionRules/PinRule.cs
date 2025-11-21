using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

public class PinRule(bool requiredPinSetStatsu) : ICardAllowedActionRule
{
    public bool IsAllowed(CardDetails cardDetails)
    {
        return cardDetails.IsPinSet == requiredPinSetStatsu;
    }
}
