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
    }

    public void AltaPersonaje (Personaje personaje)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_IdPersonaje", direction: ParameterDirection.Output);
        parametros.Add("@p_Nombre", personaje.Nombre);
        parametros.Add("@p_NombreBestia", personaje.NombreBestia);
        parametros.Add("@p_ResistenciaBestia", personaje.ResistenciaBestia);
        parametros.Add("@p_Ataques", personaje.ataques);

        _conexion.Execute("AltaPersonaje", parametros);
    }
    
    // public void ActualizarDuracionCombate (int p_idCombate,int p_Duracion ) 
    //     {
    //         var parametros: new DynamicParameters();
    //     parametros.Add("@p_IdCombate", direction: ParameterDirection.Output);
    //     parametros.Add("@p_Duracion", personaje.Nombre);
    //     parametros.Add("@p_NombreBestia", personaje.NombreBestia);

    //     }
}


