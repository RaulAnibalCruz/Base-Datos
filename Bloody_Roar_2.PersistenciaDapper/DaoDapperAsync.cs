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
            var query = "UPDATE Combate SET Duracion = @NuevaDuracion WHERE idCombate = @IdCombate";
            await _conexion.ExecuteAsync(query, new { NuevaDuracion = nuevaDuracion, IdCombate = idCombate });
        }

    public async Task AltaAtaque(Ataque ataque)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAtaque", direction: ParameterDirection.Output);
        parametros.Add("@unTipoAtaque", ataque.Tipo_Ataque);
        parametros.Add("@unDanio", ataque.Danio);
        parametros.Add("@unIdPersonaje", ataque.IdPersonaje);

        await _conexion.ExecuteAsync("AltaAtaque", parametros, commandType: CommandType.StoredProcedure);
        ataque.IdAtaque = parametros.Get<int>("unIdAtaque");
    }

 public async Task AltaCombate(Combate combate)
{
    var parametros = new DynamicParameters();
    parametros.Add("p_idCombate", dbType: DbType.Int32, direction: ParameterDirection.Output);
    parametros.Add("p_idPersonaje", combate.IdPersonaje);
    parametros.Add("p_idUsuario", combate.IdUsuario);
    parametros.Add("p_idModo_Juego", combate.IdModo_Juego);
    parametros.Add("p_Duracion", combate.Duracion);

    await _conexion.ExecuteAsync("AltaCombate", parametros, commandType: CommandType.StoredProcedure);

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

        await _conexion.ExecuteAsync("AltaPersonaje", parametros);
        personaje.IdPersonaje = parametros.Get<int>("p_IdPersonaje");
    }


    // AltaUsuario: retorna el id (o -1 si existe)
    public async Task<int> AltaUsuario(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdUsuario", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parametros.Add("unNombre", usuario.Nombre);
        parametros.Add("unaContrasenia", usuario.Contrasenia);
        parametros.Add("unEmail", usuario.Email);

        await _conexion.ExecuteAsync("AltaUsuario", parametros, commandType: CommandType.StoredProcedure);

        return parametros.Get<int>("unIdUsuario");
    }

// Buscar por email (usa SP BuscarUsuarioPorEmail)
    public async Task<Usuario?> BuscarUsuarioPorEmail(string email)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@emailBuscado", email);
        // usando QueryFirstOrDefault con commandType SP
        var usuario = await _conexion.QueryFirstOrDefaultAsync<Usuario>(
            "BuscarUsuarioPorEmail",
            parametros,
            commandType: CommandType.StoredProcedure
        );
        return usuario;
    }

    public async Task<Usuario?> ObtenerUsuarioPorLogin(string nombre, string email)
{
    var parametros = new DynamicParameters();
    parametros.Add("@Nombre", nombre);
    parametros.Add("@Email", email);

    return await _conexion.QueryFirstOrDefaultAsync<Usuario>(
        "SELECT * FROM Usuario WHERE Nombre = @Nombre AND Email = @Email",
        parametros
    );
}



    public async Task<IEnumerable<Ataque>> ObtenerAtaque()
{
    var sql = @"
        SELECT 
            a.IdAtaque,
            a.Tipo_Ataque,
            a.Danio,
            a.IdPersonaje,
            p.Nombre AS NombrePersonaje
        FROM Ataque a
        INNER JOIN Personaje p ON a.IdPersonaje = p.IdPersonaje;
    ";

    return await _conexion.QueryAsync<Ataque>(sql);
}





    public async Task<Combate?> ObtenerCombatePorId(int idCombate)
{
    var query = "SELECT * FROM Combate WHERE idCombate = @Id";
    return await _conexion.QueryFirstOrDefaultAsync<Combate>(query, new { Id = idCombate });
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
            var sql = "SELECT idUsuario AS IdUsuario, Nombre, Contrasenia, Email FROM Usuario";
            return await _conexion.QueryAsync<Usuario>(sql);
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



// Eliminar por id (si no tenés SP, usamos SQL directo)
    public async Task EliminarUsuario(int idUsuario)
    {
        var sql = "DELETE FROM Usuario WHERE idUsuario = @id";
        await _conexion.ExecuteAsync(sql, new { id = idUsuario });
    }

    public async Task EliminarPersonaje(int idPersonaje)
    {
        await _conexion.ExecuteAsync("DELETE FROM Personaje WHERE idPersonaje = @idPersonaje", new { idPersonaje });
    }

    public async Task EliminarModoJuego(int idModoJuego)
    {
        await _conexion.ExecuteAsync("DELETE FROM ModoJuego WHERE idModoJuego = @idModoJuego", new { idModoJuego });
    }

    public async Task EliminarAtaque(int id)
{
    var parametros = new DynamicParameters();
    parametros.Add("@idAtaque", id);
    await _conexion.ExecuteAsync("EliminarAtaque", parametros, commandType: CommandType.StoredProcedure);
}


}




internal class _conexion
{
}