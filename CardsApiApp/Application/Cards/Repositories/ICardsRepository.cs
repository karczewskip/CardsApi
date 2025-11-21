using CardsApiApp.Domain.Cards.ValueObjects;

namespace CardsApiApp.Application.Cards.Repositories;

public record GetCardDetailsResult(bool UserExists, bool CardExists, CardDetails? CardDetails) 
{ 
    public static GetCardDetailsResult NotFoundUser => new GetCardDetailsResult(false, false, null);
    public static GetCardDetailsResult NotFoundCard => new GetCardDetailsResult(true, false, null);
    public static GetCardDetailsResult From(CardDetails cardDetails) => new GetCardDetailsResult(true, true, cardDetails);
}

public record GetAllUserCardsResult(bool UserExists, string[] CardNumbers)
{
    public static GetAllUserCardsResult NotFoundUser => new GetAllUserCardsResult(false, Array.Empty<string>());
    public static GetAllUserCardsResult From(string[] cardNumbers) => new GetAllUserCardsResult(true, cardNumbers);

}

public interface ICardsRepository
{
    Task<GetCardDetailsResult> GetCardDetails(string userId, string cardNumber);
    Task<GetAllUserCardsResult> GetAllUserCards(string userId);
}
