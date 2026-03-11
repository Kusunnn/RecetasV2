namespace RecetArreAPI2.Models
{
    public class Rec_Tiem
    {
        public int IdReceta { get; set; }
        public int IdTiempo { get; set; }

        public Receta? Receta { get; set; }
        public Tiempo? Tiempo { get; set; }
    }
}
