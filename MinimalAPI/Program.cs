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

app.MapGet("/personajes/id", async (IDao repo,int IdPersonaje) =>
    await repo.ObtenerPersonaje(IdPersonaje));

//app.MapPatch("/combate/Actu/id",  (IDao repo, int id) =>
//   repo.ActualizarDuracionCombate(id)); 




app.Run();







