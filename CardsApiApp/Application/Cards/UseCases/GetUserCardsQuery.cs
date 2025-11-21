using CardsApiApp.Application.Cards.Repositories;

namespace CardsApiApp.Application.Cards.UseCases;

public record GetUserCardsQuery(string UserId);

public class GetUserCardsQueryHandler
{
    private readonly ICardsRepository _cardsRepository;
    public GetUserCardsQueryHandler(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }
    public async Task<string[]> Handle(GetUserCardsQuery query)
    {
        var cardNumbers = await _cardsRepository.GetAllUserCards(query.UserId);
        return cardNumbers;
    }
}
