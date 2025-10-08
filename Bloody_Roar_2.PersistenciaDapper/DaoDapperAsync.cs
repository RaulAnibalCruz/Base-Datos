using System.Data;
using Dapper;
using Bloody_Roar_2.Persistencia;


namespace Bloody_Roar_2.PersistenciaDapper;

public class DaoDapperAsync : IDao
{

    private readonly IDbConnection _conexion;

    public DaoDapperAsync(IDbConnection conexion)
        => _conexion = conexion;

    public async Task ActualizarDuracionCombate(int idCombate, int nuevaDuracion)
    {
        var query = "UPDATE Combate SET Duracion = @NuevaDuracion WHERE IdCombate = @IdCombate";
        await _conexion.ExecuteAsync(query, new { NuevaDuracion = nuevaDuracion, IdCombate = idCombate });
    }

    public async Task AltaAtaque(Ataque ataque)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAtaque", direction: ParameterDirection.Output);
        parametros.Add("@unTipoAtaque", ataque.TipoAtaque);
        parametros.Add("@unDanio", ataque.Danio);

        await _conexion.ExecuteAsync("AltaAtaque", parametros);
        ataque.IdAtaque = parametros.Get<int>("unIdAtaque");
    }

    public async Task AltaCombate(Combate combate)
    {
        var parametros = new DynamicParameters();
        parametros.Add("p_idCombate", direction: ParameterDirection.Output);
        parametros.Add("p_idPersonaje", combate.Personaje.IdPersonaje);
        parametros.Add("p_idUsuario", combate.Usuario.IdUsuario);
        parametros.Add("p_idModo_Juego", combate.ModoJuego.IdModoJuego);
        parametros.Add("p_Duracion", combate.Duracion);

        await _conexion.ExecuteAsync("AltaCombate", parametros);
        combate.IdCombate = parametros.Get<int>("p_idCombate");
    }

    public async Task AltaModoJuego(ModoJuego modoJuego)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdModoJuego", direction: ParameterDirection.Output);
        parametros.Add("unNombre", modoJuego.Nombre);

        await _conexion.ExecuteAsync("AltaModoJuego", parametros);
        modoJuego.IdModoJuego = parametros.Get<int>("unIdModoJuego");
    }

    public async Task AltaPersonaje(Personaje personaje)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_IdPersonaje", direction: ParameterDirection.Output);
        parametros.Add("@p_Nombre", personaje.Nombre);
        parametros.Add("@p_NombreBestia", personaje.NombreBestia);
        parametros.Add("@p_ResistenciaBestia", personaje.ResistenciaBestia);
        parametros.Add("@p_Ataques", personaje.ataques);

        await _conexion.ExecuteAsync("AltaPersonaje", parametros);
        personaje.IdPersonaje = parametros.Get<int>("p_IdPersonaje");
    }

    public async Task AltaUsuario(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", usuario.Nombre);
        parametros.Add("@unaContrasenia", usuario.Contrasenia);
        parametros.Add("@unEmail", usuario.Email);
        // parametros.Add("@unUltimoCombate", usuario.UltimoCombate);
        parametros.Add("@unIdUltimoCombate", usuario.UltimoCombate?.IdCombate, DbType.Int32);
        await _conexion.ExecuteAsync("AltaUsuario", parametros);

        usuario.IdUsuario = parametros.Get<int>("@unIdUsuario");
    }


    public async Task<Ataque?> ObtenerAtaque(int IdAtaque)
    {
        var query = "SELECT * FROM Ataque WHERE IdAtaque = @IdAtaque";
        return await _conexion.QueryFirstOrDefaultAsync<Ataque>(query, new { IdAtaque });
    }

    public async Task<Combate?> ObtenerCombatePorId(int idCombate)
    {
        var query = "SELECT * FROM Combate WHERE IdCombate = @IdCombate";
        return await _conexion.QueryFirstOrDefaultAsync<Combate>(query, new { IdCombate = idCombate });

    }

    public async Task<ModoJuego?> ObtenerModoJuego(int IdModoJuego)
    {
        var query = "SELECT * FROM ModoJuego WHERE IdModoJuego = @IdModoJuego";
        return await _conexion.QueryFirstOrDefaultAsync<ModoJuego>(query, new { IdModoJuego });
    }

    public async Task<Personaje?> ObtenerPersonaje(int IdPersonaje)
    {
        var query = "SELECT * FROM Personaje WHERE IdPersonaje = @IdPersonaje";
        return await _conexion.QueryFirstOrDefaultAsync<Personaje>(query, new { IdPersonaje });
    }

    public async Task<Usuario?> ObtenerUsuario(int IdUsuario)
    {
        var query = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
        return await _conexion.QueryFirstOrDefaultAsync<Usuario>(query, new { IdUsuario });
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodoUsuario()
    {
        var sql = "SELECT * FROM Usuario";
        var usuarios = await _conexion.QueryAsync<Usuario>(sql);
        return usuarios;
    }

    public async Task<IEnumerable<Personaje>> ObtenerTodoPersonaje()
    {
        var sql = "SELECT * FROM Personaje";
        var personajes = await _conexion.QueryAsync<Personaje>(sql);
        return personajes;
    }

    public async Task<IEnumerable<ModoJuego>> ObtenerTodoModoJuego()
    {
        string sql = "SELECT * FROM ModoJuego"; // suponiendo que tu tabla se llama así
        var lista = await _conexion.QueryAsync<ModoJuego>(sql);
        return lista;
    }



    public async Task EliminarUsuario(int idUsuario)
    {
        await _conexion.ExecuteAsync("DELETE FROM Usuario WHERE idUsuario = @idUsuario", new { idUsuario });
    }

    public async Task EliminarPersonaje(int idPersonaje)
    {
        await _conexion.ExecuteAsync("DELETE FROM Personaje WHERE idPersonaje = @idPersonaje", new { idPersonaje });
    }

    public async Task EliminarModoJuego(int idModoJuego)
    {
        await _conexion.ExecuteAsync("DELETE FROM ModoJuego WHERE idModoJuego = @idModoJuego", new { idModoJuego });
    }

}




internal class _conexion
{
}