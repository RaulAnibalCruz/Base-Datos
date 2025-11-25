namespace Bloody_Roar_2
{
    public class Ataque
    {
        public int IdAtaque { get; set; }
        public string Tipo_Ataque { get; set; } = "";

        //  public string TipoAtaque {get; set;} = "";
        public int Danio { get; set; }

        // Clave foránea
        public int IdPersonaje { get; set; }

        // Para mostrar el nombre del personaje al listar ataques
        public string? NombrePersonaje { get; set; }
    }
}

