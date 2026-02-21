using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.DTOs.Ingredientes
{
    public class IngredienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string? UnidadMed { get; set; }
        public string? Descripcion { get; set; }

    }

    public class IngredienteCreacionDto
    {   
        public string Nombre { get; set; } = default!;
        public string? UnidadMed { get; set; }
        public string? Descripcion { get; set; }
    }
    public class IngredienteModificacionDto
    {   
        public string Nombre { get; set; } = default!;
        public string? UnidadMed { get; set; }
        public string? Descripcion { get; set; }
    }
}
