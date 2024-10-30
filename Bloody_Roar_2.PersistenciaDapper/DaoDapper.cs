using System.Data;
using Dapper;
using Bloody_Roar_2.Persistencia;

namespace Bloody_Roar_2.PersistenciaDapper;
public class DaoDapper : IDao
{
    private readonly IDbConnection _conexion;

    public DaoDapper(IDbConnection conexion)
        => _conexion = conexion;

    public void AltaUsuario(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", usuario.Nombre);
        parametros.Add("@unaContrasenia", usuario.Contrasenia);
        parametros.Add("@unEmail", usuario.Email);
        parametros.Add("@unUltimoCombate", usuario.UltimoCombate);

        _conexion.Execute("AltaUsuario", parametros);

        usuario.IdUsuario = parametros.Get<int>("@unIdUsuario");
    }
    
    public void AltaPersonaje(Personaje personaje)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_IdPersonaje", direction: ParameterDirection.Output);
        parametros.Add("@p_Nombre", personaje.Nombre);
        parametros.Add("@p_NombreBestia", personaje.NombreBestia);
        parametros.Add("@p_ResistenciaBestia", personaje.ResistenciaBestia);
        parametros.Add("@p_Ataques", personaje.ataques);

        _conexion.Execute("AltaPersonaje", parametros);
        personaje.IdPersonaje = parametros.Get<int>("p_IdPersonaje");
    }
    public void AltaAtaque(Ataque ataque){
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAtaque", direction: ParameterDirection.Output);
        parametros.Add("@unTipoAtaque", ataque.TipoAtaque);
        parametros.Add("@unDanio", ataque.Danio);

        _conexion.Execute("AltaAtaque", parametros);
        ataque.IdAtaque = parametros.Get<int>("unIdAtaque");
    }
    public void AltaModoJuego(ModoJuego modoJuego){
        var parametros = new DynamicParameters();
        parametros.Add("@unIdModoJuego", direction: ParameterDirection.Output);
        parametros.Add("unNombre", modoJuego.Nombre);

        _conexion.Execute("AltaModoJuego", parametros);
        modoJuego.IdModoJuego = parametros.Get<int>("unIdModoJuego");
    }
    public void AltaCombate(Combate combate)
    {
        var parametros = new DynamicParameters();
        parametros.Add("p_idCombate", direction: ParameterDirection.Output);
        parametros.Add("p_idPersonaje", combate.Personaje.IdPersonaje);
        parametros.Add("p_idUsuario", combate.Usuario.IdUsuario);
        parametros.Add("p_idModo_Juego", combate.ModoJuego.IdModoJuego);
        parametros.Add("p_Duracion", combate.Duracion);

        _conexion.Execute("AltaCombate", parametros);
        combate.IdCombate = parametros.Get<int>("p_idCombate");

    }
    public void ActualizarDuracionCombate(int idCombate, int nuevaDuracion)
    {
        var query = "UPDATE Combate SET Duracion = @NuevaDuracion WHERE IdCombate = @IdCombate";
        _conexion.Execute(query, new { NuevaDuracion = nuevaDuracion, IdCombate = idCombate });
    }
    public Combate? ObtenerCombatePorId(int idCombate)
    {
        var query = "SELECT * FROM Combate WHERE IdCombate = @IdCombate";
        return _conexion.QueryFirstOrDefault<Combate>(query, new { IdCombate = idCombate });
        
    }
    public Usuario? ObtenerUsuario(int IdUsuario)
    {
        var query = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
        return _conexion.QueryFirstOrDefault<Usuario>(query, new { IdUsuario });
    }
    public Personaje? ObtenerPersonaje(int IdPersonaje)
    {
        var query = "SELECT * FROM Personaje WHERE IdPersonaje = @IdPersonaje";
        return _conexion.QueryFirstOrDefault<Personaje>(query, new { IdPersonaje } );
    }
}