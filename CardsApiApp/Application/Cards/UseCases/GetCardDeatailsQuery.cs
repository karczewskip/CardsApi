using CardsApiApp.Domain.Cards;
using CardsApiApp.Application.Cards.Repositories;

namespace CardsApiApp.Application.Cards.UseCases;

public record GetCardDeatailsQuery(string UserId, string CardNumber);

public class GetCardDeatailsQueryHandler
{
    private readonly ICardsRepository _cardsRepository;
    public GetCardDeatailsQueryHandler(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<CardDetails?> Handle(GetCardDeatailsQuery query)
    {
        return await _cardsRepository.GetCardDetails(query.UserId, query.CardNumber);
    }
}
