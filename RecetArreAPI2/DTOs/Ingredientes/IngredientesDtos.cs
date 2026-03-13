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
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(20)]
        public string? UnidadMed { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }
    public class IngredienteModificacionDto
    {   
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(20)]
        public string? UnidadMed { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }
}
