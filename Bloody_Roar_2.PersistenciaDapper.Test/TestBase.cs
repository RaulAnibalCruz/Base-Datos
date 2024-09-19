using System.Data;
using Bloody_Roar_2.Persistencia;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Bloody_Roar_2.PersistenciaDapper.Test;

public abstract class TestBase
{
    private readonly IDbConnection _conexion;
    protected IDao Dao;
    public TestBase()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
            .Build();
        string cadena = config.GetConnectionString("MySQL")!;
        _conexion = new MySqlConnection(cadena);

        Dao = new DaoDapper(_conexion);
    }
}
