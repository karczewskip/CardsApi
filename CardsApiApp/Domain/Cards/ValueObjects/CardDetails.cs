namespace CardsApiApp.Domain.Cards.ValueObjects;

public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
