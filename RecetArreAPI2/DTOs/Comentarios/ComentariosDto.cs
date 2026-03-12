// using System.ComponentModel.DataAnnotations;

// namespace RecetArreAPI2.DTOs.Comentarios
// {
//     public class ComentariosDto
//     {
//         public int Id { get; set; }

//         [Required]
//         [StringLength(500)]
//         public string TextoCom { get; set; } = default!;

//         [StringLength(100, MinimumLength = 2)]
//         public string? IdUsuario { get; set; }

//         [Required]
//         [Range(1, 5)]
//         public int Puntuacion { get; set; }

//         public int IdReceta { get; set; }

//         public DateTime Fecha { get; set; } = DateTime.UtcNow;
//     }


//     public class ComentariosCreacionDto
//     {
//         [Required]
//         [StringLength(500)]
//         public string TextoCom { get; set; } = default!;

//         [Required]
//         [Range(1, 5)]
//         public int Puntuacion { get; set; }

//         [Required]
//         public int IdReceta { get; set; }
//     }

//     public class ComentariosModificacionDto
//     {
//         [Required]
//         [StringLength(500)]
//         public string TextoCom { get; set; } = default!;

//         [Required]
//         [Range(1, 5)]
//         public int Puntuacion { get; set; }

//         [Required]
//         public int IdReceta { get; set; }
//     }
// }
