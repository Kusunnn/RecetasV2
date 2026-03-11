using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.Models
{
    public class Tiempo
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;
        
        [StringLength(500)]
        public string? Descripcion { get; set; }

        public ICollection<Rec_Tiem> RecetasTiempos { get; set; } = new List<Rec_Tiem>();
    }
}
