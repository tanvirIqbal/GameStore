using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Features.Games.UpdateGame;

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