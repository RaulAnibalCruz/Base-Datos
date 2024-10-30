using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bloody_Roar_2.PersistenciaDapper.Test;

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
    public void ObtenerModoJuegoOK()
    {
        var modoJuego = Dao.ObtenerModoJuego(1);
        Assert.NotNull(modoJuego);
        Assert.Equal(1, modoJuego.IdModoJuego);
    }
}
