using System.Runtime.CompilerServices;

namespace Bloody_Roar_2.Persistencia;

public interface IDao
{
    Task ActualizarDuracionCombate(int idCombate, int nuevaDuracion);
    Task AltaPersonaje(Personaje personaje);
    Task AltaUsuario(Usuario usuario);
    Task <Combate?> ObtenerCombatePorId(int idCombate);
    Task AltaAtaque(Ataque ataque);

    Task AltaModoJuego(ModoJuego modoJuego);
    Task AltaCombate(Combate combate);
    Task <Usuario?> ObtenerUsuario(int IdUsuario);

    Task <Personaje?> ObtenerPersonaje(int IdPersonaje);

    Task <ModoJuego?> ObtenerModoJuego(int IdModoJuego);

    Task <Ataque?> ObtenerAtaque(int IdAtaque);

    

}