using Bloody_Roar_2.Persistencia;
using Bloody_Roar_2.PersistenciaDapper;
using MySqlConnector;
using System.Data;

// ...
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

// Registrás la conexión
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

// Registrás el Dao (esto resuelve tu error)
builder.Services.AddScoped<IDao, DaoDapperAsync>();

// MVC
builder.Services.AddControllersWithViews();

// 👉 Necesario para leer HttpContext en las vistas
builder.Services.AddHttpContextAccessor();

// 👉 Necesario para usar sesión
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 👉 Activar sesión ANTES de Authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
