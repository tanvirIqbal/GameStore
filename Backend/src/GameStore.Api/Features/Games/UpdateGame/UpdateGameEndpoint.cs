using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.UpdateGame
{
    public static class UpdateGameEndpoint
    {
        public static void MapUpdateGame(this IEndpointRouteBuilder app, GameStoreData data)
        {
            //PUT /games/{id}
            app.MapPut("/{id}", (Guid id, UpdateGameDto updateGameDto) =>
            {
                var game = data.GetGame(id);
                if (game is null)
                {
                    return Results.NotFound();
                }

                var genre = data.GetGenre(updateGameDto.GenreId);
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
        }
    }
}