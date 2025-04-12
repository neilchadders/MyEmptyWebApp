using System.ComponentModel.DataAnnotations;

namespace UpdateGames.Dto;

public record class UpdateGameDto(
         [Required][StringLength(50)]string Name,
        [Required][StringLength(20)] string Genre,
        [Range(1,100)] decimal Price, // Price must be between 1 and 100
        DateOnly ReleaseDate
    );


