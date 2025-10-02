using Bloody_Roar_2.Persistencia;
using Bloody_Roar_2.PersistenciaDapper;
using MySqlConnector;
using System.Data;

// ...

var connectionString = builder.Configuration.GetConnectionString("MySQL");

// Registrás la conexión
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

// Registrás el Dao (esto resuelve tu error)
builder.Services.AddScoped<IDao, DaoDapperAsync>();

// MVC
builder.Services.AddControllersWithViews();



////
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
