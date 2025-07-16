using Bloody_Roar_2.PersistenciaDapper;
using Bloody_Roar_2.Persistencia;
using System.Threading.Tasks;
namespace Bloody_Roar_2.PersistenciaDapper.Test;
public class UsuarioTest : TestBase
{
    public UsuarioTest() : base()
    {

    }
    [Fact]

    public async Task AltaUsuarioOK()
    {
        var diego = new Usuario()
        {
            Nombre = "Diego",
            Email = "diego@quintero.com",
            Contrasenia = "123456"
        };

        Dao.AltaUsuario(diego);

        Assert.NotEqual(5, diego.IdUsuario);
    }


    [Fact]
    public async Task ObtenerUsuarioOK()
    {

        var usuario =  await Dao.ObtenerUsuario(4);

        Assert.NotNull(usuario);
        Assert.Equal(1, usuario.IdUsuario);
    }
    
}