namespace Bloody_Roar_2.PersistenciaDapper.Test;

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
    public void ObtenerAtaqueOK()
    {
        var ataque = Dao.ObtenerAtaque(1);
        Assert.NotNull(ataque);
        Assert.Equal(1, ataque.IdAtaque);
    }
}