namespace Bloody_Roar_2;

public class Combate
{
    public static object ActualizarDuracionCombateOK { get; set; }
    public int IdCombate {get; set;}
    public Personaje personaje {get; set;}
    public int IdUsuario {get; set;}
    public ModoJuego modoJuego {get; set;}
    public int Duracion {get; set;}    
}