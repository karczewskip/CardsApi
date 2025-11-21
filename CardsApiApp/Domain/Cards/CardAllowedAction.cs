namespace CardsApiApp.Domain.Cards
{
    public class CardAllowedAction(string name, ICardAllowedActionRule rule)
    {
        public string Name { get; } = name;
        public ICardAllowedActionRule Rule { get; } = rule;
    }

    public interface ICardAllowedActionRule
    {
        bool IsAllowed(CardDetails cardDetails);
    }

    public class AllowedForAllCardsRule : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            return true;
        }
    }

    public class AllowedForStatusRule(CardStatus[] requiredStatuses) : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            return requiredStatuses.Contains(cardDetails.CardStatus);
        }
    }

    public class AllowedForTypeRule(CardType[] requiredTypes) : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            return requiredTypes.Contains(cardDetails.CardType);
        }
    }

    public class AlternativeAllowedRule(ICardAllowedActionRule[] alternativeRules) : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            foreach (var rule in alternativeRules)
            {
                if (rule.IsAllowed(cardDetails))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class CompositeAllowedRule(ICardAllowedActionRule[] allRules) : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            foreach (var rule in allRules)
            {
                if (!rule.IsAllowed(cardDetails))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class PinRule(bool requiredPinSetStatsu) : ICardAllowedActionRule
    {
        public bool IsAllowed(CardDetails cardDetails)
        {
            return cardDetails.IsPinSet == requiredPinSetStatsu;
        }
    }

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
}
