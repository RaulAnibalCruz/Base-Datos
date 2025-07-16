namespace Bloody_Roar_2.PersistenciaDapper.Test;

using Bloody_Roar_2.PersistenciaDapper;
using Bloody_Roar_2.Persistencia;
public class ModoJuegoTest : TestBase
{
    public ModoJuegoTest() : base()
    {

    }
    [Fact]
    public void AltaModoJuegoOK()
    {
        var arcade = new ModoJuego()
        {
            Nombre = "Arcade"
        };

        Dao.AltaModoJuego(arcade);
        Assert.NotEqual(0, arcade.IdModoJuego);
    }
    [Fact]
    public async Task ObtenerModoJuegoOK()
    {
        var modoJuego = await Dao.ObtenerModoJuego(1); 
        Assert.NotNull(modoJuego);
        Assert.Equal(1, modoJuego!.IdModoJuego); 
    }
}
