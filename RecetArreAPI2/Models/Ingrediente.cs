using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecetArreAPI2.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(20)]
        public string? UnidadMed { get; set; }
        
        [StringLength(500)]
        public string? Descripcion { get; set; }
        public ICollection<Recetas> Recetas { get; set; } = new List<Recetas>();
    }
}
