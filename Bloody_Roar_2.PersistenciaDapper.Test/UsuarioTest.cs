namespace Bloody_Roar_2.PersistenciaDapper.Test;

public class UsuarioTest : TestBase
{
    public UsuarioTest() : base()
    {

    }
    [Fact]

    public void AltaUsuarioOK()
    {
        var diego = new Usuario()
        {
            Nombre = "Diego",
            Email = "diego@quintero.com",
            Contrasenia = "123456"
        };

        Dao.AltaUsuario(diego);

        Assert.NotEqual(0, diego.IdUsuario);
    }
}