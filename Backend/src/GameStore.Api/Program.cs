using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameStoreData data = new GameStoreData();

app.MapGamesEndpoints(data);
app.MapGenresEndpoints(data);




//app.MapGet("/", () => "Hello World!");

app.Run();









