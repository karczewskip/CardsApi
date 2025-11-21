using CardsApiApp.Application.Cards.Repositories;
using CardsApiApp.Domain.Cards;

namespace CardsApiApp.Application.Cards.UseCases
{
    public record GetAllowedActionsQuery(string UserId, string CardNumber);
    public class GetAllowedActionsQueryHandler(CardAllowedActionsProvider cardAllowedActionsProvider, ICardsRepository cardsRepository)
    {
        public async Task<string[]> Handle(GetAllowedActionsQuery query)
        {
            var cardDetails = await cardsRepository.GetCardDetails(query.UserId, query.CardNumber);
            if (cardDetails == null)
            {
                return Array.Empty<string>();
            }
            var allAllowedActions = cardAllowedActionsProvider.GetAllAllowedActions();
            var allowedActions = allAllowedActions
                .Where(action => action.Rule.IsAllowed(cardDetails))
                .Select(action => action.Name)
                .ToArray();

            return allowedActions;
        }
    }

}
