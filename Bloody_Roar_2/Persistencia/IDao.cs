using System.Runtime.CompilerServices;

namespace Bloody_Roar_2.Persistencia;

public interface IDao
{
    Task ActualizarDuracionCombate(int idCombate, int nuevaDuracion);
    Task AltaPersonaje(Personaje personaje);
    Task<int> AltaUsuario(Usuario usuario);
    Task<Combate?> ObtenerCombatePorId(int idCombate);
    Task AltaAtaque(Ataque ataque);

    Task AltaModoJuego(ModoJuego modoJuego);
    Task AltaCombate(Combate combate);
    Task<Usuario?> ObtenerUsuario(int IdUsuario);

    Task<Personaje?> ObtenerPersonaje(int IdPersonaje);

    Task<ModoJuego?> ObtenerModoJuego(int IdModoJuego);

    Task<IEnumerable<Ataque>> ObtenerAtaque();

    Task<IEnumerable<Usuario>> ObtenerTodoUsuario();

    Task<IEnumerable<Personaje>> ObtenerTodoPersonaje();

    Task<IEnumerable<ModoJuego>> ObtenerTodoModoJuego();

    Task<Usuario?> BuscarUsuarioPorEmail(string email);

    Task EliminarUsuario(int idUsuario);
    Task EliminarPersonaje(int idPersonaje);
    Task EliminarModoJuego(int idModoJuego);
    Task EliminarAtaque(int id);

    Task<Usuario?> ObtenerUsuarioPorLogin(string nombre, string email);

    Task EliminarCombate(int idCombate);

    Task<IEnumerable<Combate>> ObtenerTodoCombate();


    Task<IEnumerable<CombateCompleto>> ObtenerTodosCombatesConNombres();
    
    Task<CombateCompleto?> ObtenerCombateConNombres(int id);
}

