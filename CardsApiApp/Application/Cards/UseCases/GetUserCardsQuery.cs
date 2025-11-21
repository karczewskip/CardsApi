using CardsApiApp.Application.Cards.Repositories;

namespace CardsApiApp.Application.Cards.UseCases;

public record GetUserCardsQuery(string UserId);

public record GetUserCardsQueryResult(bool UserExists, string[] CardNumbers);

public class GetUserCardsQueryHandler
{
    private readonly ICardsRepository _cardsRepository;
    public GetUserCardsQueryHandler(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<GetAllUserCardsResult> Handle(GetUserCardsQuery query)
    {
        var result = await _cardsRepository.GetAllUserCards(query.UserId);
        return new GetAllUserCardsResult(UserExists: result.UserExists, CardNumbers: result.CardNumbers);
    }
}
