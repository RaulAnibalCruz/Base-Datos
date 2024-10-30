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
            int nuevaDuracion = 15; // Nueva duración de 15 minutos

            // Actualizar la duración del combate
            Dao.ActualizarDuracionCombate(6, nuevaDuracion);

            // Opcional: Recuperar el combate para verificar la actualización (si tienes un método para eso)
            var combateActualizado = Dao.ObtenerCombatePorId(6); 

        }
    }
    [Fact]
    public void ObtenerCombatePorIdOK()
    {
        var combate = Dao.ObtenerCombatePorId(6);

        Assert.NotNull(combate);
        Assert.Equal(0, combate.IdCombate);
    }
    [Fact]
    public void AltaCombateOk()
    {
        var combate1 = new Combate()
        {
            Personaje = new Personaje { IdPersonaje = 1 },
            Usuario = new Usuario { IdUsuario = 1 },
            ModoJuego = new ModoJuego { IdModoJuego = 1 },
            Duracion = 10
        };
        Dao.AltaCombate(combate1);
        Assert.NotEqual(0, combate1.IdCombate);
    }
}
