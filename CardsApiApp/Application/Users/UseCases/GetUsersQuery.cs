using CardsApiApp.Application.Users.Repositories;

namespace CardsApiApp.Application.Users.UseCases;

public record GetUsersQuery();

public class GetUsersQueryHandler
{
    private readonly IUsersRepository _usersRepository;
    public GetUsersQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task<string[]> Handle(GetUsersQuery query)
    {
        return await _usersRepository.GetAllUsersAsync();
    }
}
