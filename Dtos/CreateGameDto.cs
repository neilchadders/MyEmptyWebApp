using System.ComponentModel.DataAnnotations; // For the [Required] attribute

namespace CreateGame.Dto
{
    public record CreateGameDto(
        [Required][StringLength(50)]string Name,
        int GenreId, // Assuming GenreId is an integer
        [Range(1,100)] decimal Price, // Price must be between 1 and 100
        DateOnly ReleaseDate
    );
}
