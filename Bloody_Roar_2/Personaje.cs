namespace Bloody_Roar_2
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; } = "";
        public string NombreBestia { get; set; } = "";
        public int ResistenciaBestia { get; set; }

        // Relación: un personaje puede tener varios ataques
        public List<Ataque> Ataques { get; set; } = new List<Ataque>();
    }
}













//*namespace Bloody_Roar_2;

//public class Personaje
//{
 //   public int IdPersonaje { get; set; }
  //  public string Nombre { get; set; } = "";
   // public string NombreBestia { get; set; } = "";
   // public int ResistenciaBestia { get; set; }

    //public Ataque ataques {get; set;}

  //  public string ataques { get; set; } = "";
    
//}