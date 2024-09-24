using System.Runtime.CompilerServices;

namespace Bloody_Roar_2.Persistencia;

public interface IDao
{
    void ActualizarDuracionCombate(int idCombate, int nuevaDuracion);
    void AltaPersonaje(Personaje personaje);
    void AltaUsuario(Usuario usuario);
    Combate ObtenerCombatePorId(int idCombate);
    void AltaAtaque(Ataque ataque);

    void AltaModoJuego(ModoJuego modoJuego);

}