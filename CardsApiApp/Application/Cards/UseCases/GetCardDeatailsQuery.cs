using CardsApiApp.Domain.Cards;
using CardsApiApp.Application.Cards.Repositories;

namespace CardsApiApp.Application.Cards.UseCases;

public record GetCardDeatailsQuery(string UserId, string CardNumber);

public record GetCardDetailsQueryResult(bool UserExists, bool CardExists, CardDetails? CardDetails);

public class GetCardDeatailsQueryHandler
{
    private readonly ICardsRepository _cardsRepository;
    public GetCardDeatailsQueryHandler(ICardsRepository cardsRepository)
    {
        _cardsRepository = cardsRepository;
    }

    public async Task<GetCardDetailsQueryResult> Handle(GetCardDeatailsQuery query)
    {
        var result = await _cardsRepository.GetCardDetails(query.UserId, query.CardNumber);

        return new GetCardDetailsQueryResult(
            UserExists: result.UserExists,
            CardExists: result.CardExists,
            CardDetails: result.CardDetails
        );
    }
}
