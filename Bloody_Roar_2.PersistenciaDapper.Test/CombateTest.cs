namespace Bloody_Roar_2.PersistenciaDapper.Test;

public class CombateTest : TestBase
{
    public CombateTest() : base()
    {

    }

    [Fact]
    public void ActualizarDuracionCombateOK()
    {
        {
            int idCombate = 1;
            int nuevaDuracion = 15; // Nueva duración de 15 minutos

            // Actualizar la duración del combate
            Dao.ActualizarDuracionCombate(idCombate, nuevaDuracion);

            // Opcional: Recuperar el combate para verificar la actualización (si tienes un método para eso)
            var combateActualizado = Dao.ObtenerCombatePorId(idCombate); // Asegúrate de tener un método para esto

        }
    }

    [Fact]
    public void ObtenerCombatePorIdOK()
    {
        // Supón que existe un combate con IdCombate = 1
        int idCombate = 1;

        // Obtener el combate por su ID
        var combate = Dao.ObtenerCombatePorId(idCombate);

        Assert.NotNull(combate);
        Assert.Equal(idCombate, combate.IdCombate);
    }
}
