using MyEmptyWebApp.Dto;
using CreateGame.Dto;
using UpdateGames.Dto;          
using MyEmptyWebApp.Data;
using MyEmptyWebApp.Entities;


namespace MyEmptyWebApp.Endpoints;

public static class Endpoints

{
  
    const string GetGameEndpointName = "GetGame"; //This replaces the hardcoded string in the MapGet method. This is a good practice to avoid typos and make it easier to change the name in one place if needed.
    public static readonly List<GameDto> games = [ // Dummy data for the sake of example. In a real application, this would be replaced with a database or other data source.

    new ( 1, "Game 1", "Action", 69.99M, new DateOnly(2023, 10, 1)),
    new ( 2, "Game 2", "Adventure", 49.99M, new DateOnly(2023, 10, 1)), 
    new ( 3, "Game 3", "Puzzle", 19.99M, new DateOnly(2023, 10, 1))
];

public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
{
    var group = app.MapGroup("games") // Grouping the endpoints under "games". Can replace "games"
    //  a "/"  so app.MapGet("/games") will become  group.MapGet("/",
                    .WithParameterValidation(); // This is a custom extension method from MinimalApisExtension
                    //                               to validate 
                    //                              the parameters. You can implement this method 
                    //                              to check if the parameters are 
                    //                              valid before processing the request.

    //GET ALL GAMES
    group.
    MapGet("/", () => games);

    // GET GAME BY ID
    group.MapGet("/{id}", (int id) =>
    {
        GameDto? game = games.Find(game => game.Id == id); //GameDto? is nullable this means it can be null
        // if game is null, return 404 Not Found, else return 200 OK with the game data

        return game is null ? Results.NotFound() : Results.Ok(game); // 404 Not Found or 200 OK
    })
    .WithName(GetGameEndpointName);

    //ADD NEW GAME

    group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
    {
       Game game = new()
       {
            Name = newGame.Name,
            Genre = dbContext.Genres.Find(newGame.GenreId), // Assuming GenreId is a foreign key in the Game table
            GenreId = newGame.GenreId,
            Price = newGame.Price,
            ReleaseDate = newGame.ReleaseDate
        };
        dbContext.Games.Add(game);
        dbContext.SaveChanges();
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game); // 201 Created
        // 201 Created with the location of the new resource in the Location header
    });

    //Update Games
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>//two parameters
    {

        var index = games.FindIndex(game => game.Id == id);

        if (index == -1)
        {
            return Results.NotFound();
        }

        games[index] = new GameDto(
            id,
            updatedGame.Name,
            updatedGame.Genre,
            updatedGame.Price,
            updatedGame.ReleaseDate
        );

        return Results.NoContent();// 204 No Content
    });

    group.MapDelete("/{id}", (int id) =>
    {
        var index = games.FindIndex(game => game.Id == id);
        if (index == -1)
        {
            return Results.NotFound();
        }
        games.RemoveAt(index);
        return Results.NoContent(); // 204 No Content
    });


    return group; 

}
}
