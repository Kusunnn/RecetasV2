// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace RecetArreAPI2.Models
// {
//     public class Comentario
//     {
//         public int Id { get; set; }

//         [Required]
//         [StringLength(500)]
//         public string TextoCom { get; set; } = default!;

//         [ForeignKey("ApplicationUser")]
//         public string? IdUsuario { get; set; }

//         [Required]
//         [Range(1, 5)]
//         public int Puntuacion { get; set; }

//         [ForeignKey("Receta")]
//         public int IdReceta { get; set; }

//         public DateTime Fecha { get; set; } = DateTime.UtcNow;

//         public ApplicationUser? Usuario { get; set; }
//         public Receta? Receta { get; set; }
//     }
// }
