using System.Data;
using Dapper;
using Bloody_Roar_2.Persistencia;

namespace Bloody_Roar_2.PersistenciaDapper;

public class DaoDapperasyng : IDao
{
    public Task ActualizarDuracionCombate(int idCombate, int nuevaDuracion)
    {
        throw new NotImplementedException();
    }

    public Task AltaAtaque(Ataque ataque)
    {
        throw new NotImplementedException();
    }

    public Task AltaCombate(Combate combate)
    {
        throw new NotImplementedException();
    }

    public Task AltaModoJuego(ModoJuego modoJuego)
    {
        throw new NotImplementedException();
    }

    public Task AltaPersonaje(Personaje personaje)
    {
        throw new NotImplementedException();
    }

    public async Task AltaUsuario (Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", usuario.Nombre);
        parametros.Add("@unaContrasenia", usuario.Contrasenia);
        parametros.Add("@unEmail", usuario.Email);
        parametros.Add("@unUltimoCombate", usuario.UltimoCombate);

        await _conexion.ExecuteAsing("AltaUsuario", parametros);

        usuario.IdUsuario = parametros.Get<int>("@unIdUsuario");
    }

    public Task<Ataque?> ObtenerAtaque(int IdAtaque)
    {
        throw new NotImplementedException();
    }

    public Task <Combate?> ObtenerCombatePorId(int idCombate)
    {
        throw new NotImplementedException();
    }

    public Task <ModoJuego?> ObtenerModoJuego(int IdModoJuego)
    {
        throw new NotImplementedException();
    }

    public Task <Personaje?> ObtenerPersonaje(int IdPersonaje)
    {
        throw new NotImplementedException();
    }

    public Task <Usuario?> ObtenerUsuario(int IdUsuario)
    {
        throw new NotImplementedException();
    }
}
