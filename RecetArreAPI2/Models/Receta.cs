using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecetArreAPI2.Models
{
    public class Receta
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(3000)]
        public string? Instrucciones { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? IdUsuario { get; set; }

        public DateTime CreadoUtc { get; set; } = DateTime.UtcNow;

        public ApplicationUser? Usuario { get; set; }
        public ICollection<Rec_Tiem> IdTiempo { get; set; } = new List<Rec_Tiem>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Rec_Cat> RecetasCategorias { get; set; } = new List<Rec_Cat>();
    }
}
