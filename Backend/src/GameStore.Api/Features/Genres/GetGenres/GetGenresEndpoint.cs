using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres.GetGenres
{
    public static class GetGenresEndpoint
    {
        public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
        {
            //GET /genres
            app.MapGet("/", () => data.GetGenres().Select(g => new GenreDto(g.Id, g.Name)));
        }
    }
}