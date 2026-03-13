using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.DTOs.Categorias
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string? Descripcion { get; set; }
        public DateTime CreadoUtc { get; set; }
    }

    public class CategoriaCreacionDto
    {   
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }

    public class CategoriaModificacionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(500)]
        public string? Descripcion { get; set; }
    }

}
