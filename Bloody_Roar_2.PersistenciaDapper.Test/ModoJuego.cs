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
    }
}
