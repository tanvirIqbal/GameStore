using System.ComponentModel.DataAnnotations;
using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGamesEndpointName = "GetGames";

List<Genre> genres = new List<Genre>
{
    new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab63e"), Name = "RPG" },
    new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab63f"), Name = "Action RPG" },
    new Genre { Id = new Guid("01522711-15d7-4218-9f03-a82d5ddab640"), Name = "Action-adventure" }
};

List<Game> games = new List<Game>
{
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "The Witcher 3: Wild Hunt",
        Genre = genres[0],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2015, 5, 19),
        Description = "The Witcher 3: Wild Hunt is an action role-playing game developed and published by CD Projekt. It is the third installment in The Witcher series and is based on the book series of the same name by Polish author Andrzej Sapkowski. The game follows the story of Geralt of Rivia, a monster hunter known as a Witcher, as he searches for his adopted daughter Ciri, who is on the run from the Wild Hunt, a group of spectral riders. The game features an open world environment, a branching narrative with multiple endings, and a combat system that combines swordplay and magic."
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Elden Ring",
        Genre = genres[1],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2022, 2, 25),
        Description = "Elden Ring is an action role-playing game developed by FromSoftware and published by Bandai Namco Entertainment. It is directed by Hidetaka Miyazaki and produced by George R.R. Martin."
    },
    new Game
    {
        Id = Guid.NewGuid(),
        Name = "Cyberpunk 2077",
        Genre = genres[2],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2020, 12, 10),
        Description = "Cyberpunk 2077 is an action role-playing video game developed by CD Projekt RED and published by Bandai Namco Entertainment. It is set in the cyberpunk universe of the tabletop role-playing game Cyberpunk RED."
    }
};

//GET /games
app.MapGet("/games", () => games.Select(g => new GameSummaryDto(
    g.Id,
    g.Name,
    g.Genre.Name,
    g.Price,
    g.ReleaseDate
)));
//GET /games/{id}
app.MapGet("/games/{id}", (Guid id) =>
{
    var game = games.Find(g => g.Id == id);
    return game is not null
    ? Results.Ok(
        new GameDetailsDto(
            game.Id,
            game.Name,
            game.Genre.Id,
            game.Price,
            game.ReleaseDate,
            game.Description)
    )
    : Results.NotFound();
}).WithName(GetGamesEndpointName);
//POST /games
app.MapPost("/games", (CreateGameDto createGameDto) =>
{
    var genre = genres.Find(g => g.Id == createGameDto.GenreId);
    if (genre is null)
    {
        return Results.BadRequest("Invalid genre ID.");
    }

    var game = new Game
    {
        Id = Guid.NewGuid(),
        Name = createGameDto.Name,
        Genre = genre,
        Price = createGameDto.Price,
        ReleaseDate = createGameDto.ReleaseDate,
        Description = createGameDto.Description
    };

    games.Add(game);
    return Results.CreatedAtRoute(GetGamesEndpointName,
    new { id = game.Id }, new GameDetailsDto(
        game.Id,
        game.Name,
        game.Genre.Id,
        game.Price,
        game.ReleaseDate,
        game.Description)
    );
}).WithParameterValidation();
//PUT /games/{id}
app.MapPut("/games/{id}", (Guid id, UpdateGameDto updateGameDto) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    var genre = genres.Find(g => g.Id == updateGameDto.GenreId);
    if (genre is null)
    {
        return Results.BadRequest("Invalid genre ID.");
    }
    game.Name = updateGameDto.Name;
    game.Genre = genre;
    game.Price = updateGameDto.Price;
    game.ReleaseDate = updateGameDto.ReleaseDate;
    game.Description = updateGameDto.Description;
    return Results.NoContent();
}).WithParameterValidation();
//DELETE /games/{id}
app.MapDelete("/games/{id}", (Guid id) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    games.Remove(game);
    return Results.NoContent();
});

//GET /genres
app.MapGet("/genres", () => genres.Select(g => new GenreDto(g.Id, g.Name)));
//app.MapGet("/", () => "Hello World!");

app.Run();

public record GameDetailsDto(
    Guid Id,
    string Name,
    Guid GenreId,
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);

public record CreateGameDto(
    [Required]
    [StringLength(50, MinimumLength = 1)]
    string Name,
    Guid GenreId,
    [Range(1, 100)]
    decimal Price,
    DateOnly ReleaseDate,
    [Required]
    [StringLength(200, MinimumLength = 10)]
    string Description);

public record UpdateGameDto(
    [Required]
    [StringLength(50, MinimumLength = 1)]
    string Name,
    Guid GenreId,
    [Range(1, 100)]
    decimal Price,
    DateOnly ReleaseDate,
    [Required]
    [StringLength(200, MinimumLength = 10)]
    string Description);

public record GenreDto(
    Guid Id,
    string Name);
