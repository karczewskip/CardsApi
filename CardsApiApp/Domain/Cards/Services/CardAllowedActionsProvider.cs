using CardsApiApp.Domain.Cards.Entities;
using CardsApiApp.Domain.Cards.ValueObjects;
using CardsApiApp.Domain.Cards.ValueObjects.CardAllowedActionRules;

namespace CardsApiApp.Domain.Cards.Services;

public class CardAllowedActionsProvider
{
    public CardAllowedAction[] GetAllAllowedActions()
    {
        return
        [
            new CardAllowedAction(
                "ACTION1",
                new AllowedForStatusRule([CardStatus.Active])
            ),
            new CardAllowedAction(
                "ACTION2",
                new AllowedForStatusRule([CardStatus.Inactive])
            ),
            new CardAllowedAction(
                "ACTION3",
                new AllowedForAllCardsRule()
            ),
            new CardAllowedAction(
                "ACTION4",
                new AllowedForAllCardsRule()
            ),
            new CardAllowedAction(
                "ACTION5",
                new AllowedForTypeRule([CardType.Credit])
            ),
            new CardAllowedAction(
                "ACTION6",
                new CompositeAllowedRule(
                    [
                        new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked]),
                        new PinRule(true)
                    ]
                )
            ),
            new CardAllowedAction(
                "ACTION7",
                new AlternativeAllowedRule(
                [
                    new CompositeAllowedRule(
                        [
                            new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active]),
                            new PinRule(false)
                        ]
                    ),
                    new CompositeAllowedRule(
                        [
                            new AllowedForStatusRule([CardStatus.Blocked]),
                            new PinRule(true)
                        ]
                    )
                ])
            ),
            new CardAllowedAction(
                "ACTION8",
                new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active, CardStatus.Blocked])
            ),
            new CardAllowedAction(
                "ACTION9",
                new AllowedForAllCardsRule()
            ),
            new CardAllowedAction(
                "ACTION10",
                new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active])
            ),
            new CardAllowedAction(
                "ACTION11",
                new AllowedForStatusRule([CardStatus.Inactive, CardStatus.Active])
            ),
            new CardAllowedAction(
                "ACTION12",
                new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active])
            ),
            new CardAllowedAction(
                "ACTION13",
                new AllowedForStatusRule([CardStatus.Ordered, CardStatus.Inactive, CardStatus.Active])
            )


        ];
    }
}
