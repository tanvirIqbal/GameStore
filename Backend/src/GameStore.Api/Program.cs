using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGamesEndpointName = "GetGames";
List<Game> games = new List<Game>
{
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "The Witcher 3: Wild Hunt",
        Genre = "RPG",
        Price = 59.99m,
        ReleaseDate = new DateOnly(2015, 5, 19)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Elden Ring",
        Genre = "Action RPG",
        Price = 59.99m,
        ReleaseDate = new DateOnly(2022, 2, 25)
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Cyberpunk 2077",
        Genre = "Action RPG",
        Price = 59.99m,
        ReleaseDate = new DateOnly(2020, 12, 10)
    }
};

//GET /games
app.MapGet("/games", () => games);
//GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.Find(g => g.Id == id);
    return game is not null ? Results.Ok(game) : Results.NotFound();
}).WithName(GetGamesEndpointName);
//POST /games
app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.NewGuid();
    games.Add(game);
    return Results.CreatedAtRoute(GetGamesEndpointName, new { id = game.Id }, game);
}).WithParameterValidation();
//app.MapGet("/", () => "Hello World!");

app.Run();
