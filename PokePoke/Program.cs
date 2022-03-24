using Microsoft.OpenApi.Models;
using PokePoke;
using PokePoke.Business.DTO;
using PokePoke.Business.Interfaces;
using PokePoke.Business.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PokeSettings>(new PokeSettings() { ConnectionString= "server=sql10.freesqldatabase.com;user=sql10481178;password=6CfKYRqhVE;database=sql10481178;", ToClose= 20 });
Ioc.RegisterServices(builder.Services);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
             builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PokePoke API", Description = "Possibilita Troca de Pokemons", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokePoke API v1");
});

app.UseCors();

app.MapPost("/change", async (List<CollectionDTO> coll, IPokemonUseCase pokemonUseCase) => {

    try
    {
        var change = await pokemonUseCase.DoChange(coll);
        if (change)
        {
            return Results.Ok();
        }
        var erros = new Dictionary<string, string[]>();
        erros["Server01"] = new string[] { "Transação completa não foi possivel" };
        return Results.ValidationProblem(erros);
    }catch (Exception ex)
    {
        var erros = new Dictionary<string, string[]>();
        erros["Server02"] = new string[] { ex.Message };
        return Results.ValidationProblem(erros);
    }
   
});


app.MapGet("/users", async (IPokemonRepository pokemonUseCase) => {

    try
    {
        var users = await pokemonUseCase.GetUserAll();
        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        var erros = new Dictionary<string, string[]>();
        erros["Server02"] = new string[] { ex.Message };
        return Results.ValidationProblem(erros);
    }

});

app.MapGet("/pokemons/{userId}", async (int userId, IPokemonRepository pokemonUseCase) => {

    try
    {
        var users = await pokemonUseCase.GetCollectionByUserId(userId);
        return Results.Ok(users);
    }
    catch (Exception ex)
    {
        var erros = new Dictionary<string, string[]>();
        erros["Server02"] = new string[] { ex.Message };
        return Results.ValidationProblem(erros);
    }

});

app.Run();
