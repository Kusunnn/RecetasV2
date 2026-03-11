namespace RecetArreAPI2.Models
{
    public class Rec_Cat
    {
        public int IdReceta { get; set; }
        public int IdCategoria { get; set; }

        public Receta? Receta { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
