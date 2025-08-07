using System.Data;
using MySqlConnector;
using Bloody_Roar_2.Persistencia;
using Bloody_Roar_2.PersistenciaDapper;
using Scalar.AspNetCore;
using Bloody_Roar_2;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));


builder.Services.AddScoped<IDao ,DaoDapperAsync>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.MapGet("/personaje/{IdPersonaje}", async (IDao repo,int IdPersonaje) =>
    await repo.ObtenerPersonaje(IdPersonaje));


app.MapGet("/Usuario/{IdUsuario}", async (IDao repo,int IdUsuario) =>
    await repo.ObtenerUsuario(IdUsuario));


app.MapPost("/NewPersonaje", async (IDao repo , Personaje personaje ) =>
{
    await repo.AltaPersonaje(personaje);
    return Results.Created($"/NewPersonaje/{personaje.IdPersonaje}", personaje);
});

app.MapPost("/NewUsuario", async (IDao repo,Usuario usuario ) =>
{
    await repo.AltaUsuario(usuario);

    return Results.Created($"/NewUsuario/{usuario.IdUsuario}",usuario);
});

app.MapGet("/TodoPersonaje", async (IDao repo) =>
{
    var lista = await repo.ObtenerTodoPersonaje();
    return Results.Ok(lista);
});

app.MapGet("/TodoUsuario", async (IDao repo ) =>
{
    var lista = await repo.ObtenerTodoUsuario();
    return Results.Ok(lista);
});

app.Run();







