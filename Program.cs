using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (1, "street fighter", "fighting", 19.99M, new DateOnly(1991, 8, 30)),
    new (2, "BRAWL stars", "rpg", 69.99M, new DateOnly(2024, 1, 20)),
    new (3, "Astro Bot", "platformer", 59.99M, new DateOnly(2007, 2, 10)),
];

// GET /games 
app.MapGet("/games", () => games);


// Get /games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id))
   .WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new (
        games.Count + 1,
        newGame.Name, 
        newGame.Genre, 
        newGame.Price, 
        newGame.ReleaseDate
        );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
});

app.Run();
