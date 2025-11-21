using CardsApiApp.Application.Cards.Repositories;
using CardsApiApp.Application.Cards.UseCases;
using CardsApiApp.Application.Users.Repositories;
using CardsApiApp.Application.Users.UseCases;
using CardsApiApp.Domain.Cards;
using CardsApiApp.Mocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.InNamespaces("CardsApiApp.Application.Users.UseCases").Where(type => type.Name.EndsWith("Handler")))
    .AsSelf()
    .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.InNamespaces("CardsApiApp.Application.Cards.UseCases").Where(type => type.Name.EndsWith("Handler")))
    .AsSelf()
    .WithScopedLifetime());

builder.Services.AddScoped<IUsersRepository, CardService>();
builder.Services.AddScoped<ICardsRepository, CardService>();
builder.Services.AddScoped<CardAllowedActionsProvider>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users", (GetUsersQueryHandler handler) =>
{
    var query = new GetUsersQuery();
    return handler.Handle(query);
});

app.MapGet("/users/{userId}/cards", (string userId, GetUserCardsQueryHandler handler) =>
{
    var query = new GetUserCardsQuery(userId);
    return handler.Handle(query);
});

app.MapGet("/users/{userId}/cards/{cardNumber}", (string userId, string cardNumber, GetCardDeatailsQueryHandler handler) =>
{
    var query = new GetCardDeatailsQuery(userId, cardNumber);
    return handler.Handle(query);
});

app.MapGet("/users/{userId}/cards/{cardNumber}/allowedActions", (string userId, string cardNumber, GetAllowedActionsQueryHandler handler) =>
{
    var query = new GetAllowedActionsQuery(userId, cardNumber);
    return handler.Handle(query);
});

app.Run();
