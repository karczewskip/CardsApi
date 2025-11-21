using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Domain.Cards.Entities;

public class CardAllowedAction(string name, ICardAllowedActionRule rule)
{
    public string Name { get; } = name;
    public ICardAllowedActionRule Rule { get; } = rule;
}
