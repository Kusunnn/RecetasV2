using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.DTOs.Identity
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
