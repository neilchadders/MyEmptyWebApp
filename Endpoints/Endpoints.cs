using MyEmptyWebApp.Dto;
using CreateGame.Dto;
using UpdateGames.Dto;


namespace MyEmptyWebApp.Endpoints;

public static class Endpoints

{
  
    const string GetGameEndpointName = "GetGame";
    public static readonly List<GameDto> games = [

    new ( 1, "Game 1", "Action", 69.99M, new DateOnly(2023, 10, 1)),
    new ( 2, "Game 2", "Adventure", 49.99M, new DateOnly(2023, 10, 1)), 
    new ( 3, "Game 3", "Puzzle", 19.99M, new DateOnly(2023, 10, 1))
];

public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
{
    var group = app.MapGroup("games");
    //GET ALL GAMES
    group.MapGet("/", () => games);

    // GET GAME BY ID
    group.MapGet("/{id}", (int id) =>
    {
        GameDto? game = games.Find(game => game.Id == id);

        return game is null ? Results.NotFound() : Results.Ok(game); // 404 Not Found or 200 OK
    })
    .WithName(GetGameEndpointName);

    //ADD NEW GAME

    group.MapPost("/", (CreateGameDto newGame) =>
    {
        GameDto game = new(
            games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
        games.Add(game);
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

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
