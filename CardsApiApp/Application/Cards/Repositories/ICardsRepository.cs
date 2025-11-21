using CardsApiApp.Domain.Cards;

namespace CardsApiApp.Application.Cards.Repositories;

public interface ICardsRepository
{
    Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
    Task<string[]> GetAllUserCards(string userId);
}
