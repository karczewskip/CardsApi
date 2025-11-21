namespace CardsApiApp.Application.Users.Repositories;

public interface IUsersRepository
{
    Task<string[]> GetAllUsersAsync();
}
