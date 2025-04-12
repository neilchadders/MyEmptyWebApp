using System.ComponentModel.DataAnnotations; // For the [Required] attribute

namespace CreateGame.Dto
{
    public record CreateGameDto(
        [Required][StringLength(50)]string Name,
        [Required][StringLength(20)] string Genre,
        [Range(1,100)] decimal Price, // Price must be between 1 and 100
        DateOnly ReleaseDate
    );
}
