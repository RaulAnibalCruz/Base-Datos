using System.Runtime.CompilerServices;

namespace Bloody_Roar_2.Persistencia;

public interface IDao
{
    void AltaPersonaje(Personaje personaje);
    void AltaUsuario(Usuario usuario);
}