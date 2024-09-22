using System.Data;
using Bloody_Roar_2.Persistencia;
using Dapper;
using MySqlConnector;
using Xunit;

namespace Bloody_Roar_2.PersistenciaDapper.Test
{
    public class CombateTest : TestBase
    { 
        public CombateTest() : base()
        {
        }

        [Fact]
        public void ActualizarDuracionCombateOK()
        {
            using (var _conexion = new MySqlConnection("Server=localhost;Port=3307;Database=5to_Bloody_Roar_2;User ID=root;Password=Trigg3rs!;"))
            {
                _conexion.Open();

                var sql = "UPDATE Combate SET Duracion = @Duracion WHERE IdCombate = @IdCombate";

                var parametros = new 
                {
                    Duracion = new TimeSpan(0, 5, 0),
                    IdCombate = 1 
                };

                int rowsAffected = _conexion.Execute(sql, parametros);

                Assert.True(rowsAffected > 0, "No se actualizó ningún combate.");
            }
        }
    }
}



   