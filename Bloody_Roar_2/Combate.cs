using System.Text.Json.Serialization;
namespace Bloody_Roar_2;

public class Combate
{
    public int IdCombate { get; set; }
    public int IdPersonaje { get; set; }
    public int IdUsuario { get; set; }
    public int IdModo_Juego { get; set; }
    public int Duracion { get; set; }
}

