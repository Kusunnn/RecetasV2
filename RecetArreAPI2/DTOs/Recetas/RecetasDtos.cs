using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.DTOs.Recetas
{
    public class RecetasDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(3000)]
        public string? Instrucciones { get; set; }
        public DateTime CreadoUtc { get; set; }
        public string? IdUsuario { get; set; }
        public List<int> TiemposIds { get; set; } = new();
        public List<int> CategoriasIds { get; set; } = new();

    }

    public class RecetasCreacionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(3000)]
        public string? Instrucciones { get; set; }

        public List<int> TiemposIds { get; set; } = new();
        public List<int> CategoriasIds { get; set; } = new();
    }

    public class RecetasModificacionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Nombre { get; set; } = default!;

        [StringLength(3000)]
        public string? Instrucciones { get; set; }

        public List<int> TiemposIds { get; set; } = new();
        public List<int> CategoriasIds { get; set; } = new();
    }
}
