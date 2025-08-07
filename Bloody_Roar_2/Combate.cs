using System.Text.Json.Serialization;
namespace Bloody_Roar_2;

public class Combate
{
    public int IdCombate { get; set; }
    public Personaje Personaje { get; set; }

    [JsonIgnore]
    public Usuario Usuario { get; set; }
    public ModoJuego ModoJuego { get; set; }
    public int Duracion { get; set; }

}
