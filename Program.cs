using MyEmptyWebApp.Endpoints;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGameEndpoints(); // Call the extension method to map the endpoints

app.Run();
