using CardsApiApp.Application.Cards.Repositories;
using CardsApiApp.Domain.Cards;

namespace CardsApiApp.Application.Cards.UseCases
{
    public record GetAllowedActionsQuery(string UserId, string CardNumber);
    public record GetAllowedActionsQueryResult(bool UserExists, bool CardExists, string[] AllowedActions);
    public class GetAllowedActionsQueryHandler(CardAllowedActionsProvider cardAllowedActionsProvider, ICardsRepository cardsRepository)
    {
        public async Task<GetAllowedActionsQueryResult> Handle(GetAllowedActionsQuery query)
        {
            var getCardDetailsResult = await cardsRepository.GetCardDetails(query.UserId, query.CardNumber);
            if ((getCardDetailsResult.UserExists && getCardDetailsResult.CardExists) == false)
            {
                return new GetAllowedActionsQueryResult(getCardDetailsResult.UserExists, getCardDetailsResult.CardExists, Array.Empty<string>());
            }

            var allAllowedActions = cardAllowedActionsProvider.GetAllAllowedActions();
            var allowedActions = allAllowedActions
                .Where(action => action.Rule.IsAllowed(getCardDetailsResult.CardDetails!))
                .Select(action => action.Name)
                .ToArray();

            return new GetAllowedActionsQueryResult(true, true, allowedActions);
        }
    }

}
