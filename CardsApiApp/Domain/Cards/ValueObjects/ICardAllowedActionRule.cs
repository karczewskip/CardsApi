namespace CardsApiApp.Domain.Cards.ValueObjects;

public interface ICardAllowedActionRule
{
    bool IsAllowed(CardDetails cardDetails);
}
