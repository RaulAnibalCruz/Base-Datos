namespace Bloody_Roar_2.PersistenciaDapper.Test;

using Bloody_Roar_2.PersistenciaDapper;
using Bloody_Roar_2.Persistencia;
public class AtaqueTest : TestBase
{
     public AtaqueTest() : base()
    {

    }
    [Fact]

    public void AltaAtaqueOK()
    {
        var especial = new Ataque()
        {
            TipoAtaque = "Especial",
            Danio = 10
        };

        Dao.AltaAtaque(especial);

        Assert.NotEqual(5, especial.IdAtaque);
    }


   [Fact]
    public async Task ObtenerAtaqueOK()
    
    {
    
        var ataque =  await Dao.ObtenerAtaque(1);
        Assert.NotNull(ataque);
        Assert.Equal(1, ataque.IdAtaque);
    }
}