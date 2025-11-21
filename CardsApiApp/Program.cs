using CardsApiApp.Application.Cards.Repositories;
using CardsApiApp.Application.Cards.UseCases;
using CardsApiApp.Application.Users.Repositories;
using CardsApiApp.Application.Users.UseCases;
using CardsApiApp.Domain.Cards.Services;
using CardsApiApp.Mocks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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

app.MapGet("/", () => "Hello from User cards api! For checking actions please use: /users/{userId}/cards/{cardNumber}/allowedActions");

app.MapGet("/users", (GetUsersQueryHandler handler) =>
{
    var query = new GetUsersQuery();
    var result = handler.Handle(query);

    return Results.Ok(new { Users = result });
});

app.MapGet("/users/{userId}/cards", (string userId, GetUserCardsQueryHandler handler) =>
{
    var query = new GetUserCardsQuery(userId);
    var result = handler.Handle(query);

    if (result.Result.UserExists == false)
    {
        return Results.NotFound(new { Message = "User not found" });
    }

    return Results.Ok(new { CardNumbers = result.Result.CardNumbers });
});

app.MapGet("/users/{userId}/cards/{cardNumber}", (string userId, string cardNumber, GetCardDeatailsQueryHandler handler) =>
{
    var query = new GetCardDeatailsQuery(userId, cardNumber);
    var result = handler.Handle(query);

    if (result.Result.UserExists == false)
    {
        return Results.NotFound(new { Message = "User not found" });
    }

    if (result.Result.CardExists == false)
    {
        return Results.NotFound(new { Message = "Card not found" });
    }

    return Results.Ok(result.Result.CardDetails);
});

app.MapGet("/users/{userId}/cards/{cardNumber}/allowedActions", (string userId, string cardNumber, GetAllowedActionsQueryHandler handler) =>
{
    var query = new GetAllowedActionsQuery(userId, cardNumber);
    var result = handler.Handle(query);

    if (result.Result.UserExists == false)
    {
        return Results.NotFound(new { Message = "User not found" });
    }

    if (result.Result.CardExists == false)
    {
        return Results.NotFound(new { Message = "Card not found" });
    }

    return Results.Ok(new { AllowedActions = result.Result.AllowedActions });
});

app.Run();
