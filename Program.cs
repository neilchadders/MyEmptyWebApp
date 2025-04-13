using MyEmptyWebApp.Endpoints;
using MyEmptyWebApp.Data;


var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGameEndpoints(); // Call the extension method to map the endpoints

app.Run();
