using MyEmptyWebApp.Dto;
using CreateGame.Dto;
using UpdateGames.Dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [

    new ( 1, "Game 1", "Action", 69.99M, new DateOnly(2023, 10, 1)),
    new ( 2, "Game 2", "Adventure", 49.99M, new DateOnly(2023, 10, 1)), 
    new ( 3, "Game 3", "Puzzle", 19.99M, new DateOnly(2023, 10, 1))
];
 

//GET ALL GAMES
app.MapGet("games", () => games);

// GET GAME BY ID
app.MapGet("games/{id}", (int id)=>

games.Find(game => game.Id ==id))

.WithName(GetGameEndpointName);

//ADD NEW GAME

app.MapPost("games", (CreateGameDto newGame) =>
{
  GameDto game = new(
    games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
  games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game); 
    
    });

//Update Games
app.MapPut("games/{id}", (int id , UpdateGameDto updatedGame) =>//two parameters
{
    // Find the game by id
    var game = games.Find(game => game.Id == id);
    if (game is null) return Results.NotFound(); // 404 Not Found

    // Update the game
    // Find the index of the game to be updated 

    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
      id,
      updatedGame.Name,
      updatedGame.Genre,
      updatedGame.Price,
      updatedGame.ReleaseDate
    );

    return Results.NoContent();// 204 No Content
});

app.MapDelete("games/{id}", (int id) =>
{
 
    // Remove the game from the list
    games.RemoveAll(game =>game.Id == id);

    return Results.NoContent(); // 204 No Content
});


app.Run();
