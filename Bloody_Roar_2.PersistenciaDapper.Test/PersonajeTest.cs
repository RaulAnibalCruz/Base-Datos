using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bloody_Roar_2.PersistenciaDapper.Test;

public class PersonajeTest : TestBase
{
    public PersonajeTest() : base()
    {

    }
    [Fact]
    public void AltaPersonajeOK()
    {
        var yugo = new Personaje()
        {
            Nombre = "Yugo",
            NombreBestia = "Lobo",
            ResistenciaBestia = 10
        };

        Dao.AltaPersonaje(yugo);
        Assert.NotEqual(0, yugo.IdPersonaje);
    }

    [Fact]
    public void ObtenerPersonajeOK()
    {
        var personaje = Dao.ObtenerPersonaje(8);
        Assert.NotNull(personaje);
        Assert.Equal(1, personaje.IdPersonaje);

    }
}
