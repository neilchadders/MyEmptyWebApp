using System;

namespace MyEmptyWebApp.Entities;

public class Game
{
    public int Id {get; set; }
    public required string Title { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; } // Navigation property to Genre

    public DateOnly ReleaseDate { get; set;}
}

