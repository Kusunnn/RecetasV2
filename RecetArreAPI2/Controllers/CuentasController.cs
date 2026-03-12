using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecetArreAPI2.DTOs.Identity;
using RecetArreAPI2.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    //www.localhost.com/api/cuentas
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        // Dar de alta un usuario
        private readonly UserManager<ApplicationUser> userManager;
        // Para ir a appsetings.json y recuperar LlaveJWT
        private readonly IConfiguration configuration;
        // Dar de alta un usuario
        private readonly SignInManager<ApplicationUser> signInManager;

        public CuentasController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        //Dar de alta un usuario
        //www.localhost.com/api/cuentas/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new ApplicationUser
            {
                UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email
            };
            var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(usuario);
            }
            return BadRequest(resultado.Errors);
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(ApplicationUser usuario)
        {
            var usuarioId = usuario.Id;

            var claims = new List<Claim>()
            {
                new Claim("email", usuario.Email ?? string.Empty),
                new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, usuarioId),
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId)
            };

            // Obtener roles del usuario
            var roles = await userManager.GetRolesAsync(usuario);
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveJWT"]!));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null,
                claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
                UsuarioId = usuarioId,
            };
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(x => x.Type == "email").FirstOrDefault();

            if (emailClaim is null || string.IsNullOrWhiteSpace(emailClaim.Value))
            {
                return Unauthorized(new { mensaje = "Token inválido: no contiene email." });
            }

            var usuario = await userManager.FindByEmailAsync(emailClaim.Value);
            if (usuario is null)
            {
                return Unauthorized(new { mensaje = "Usuario no encontrado." });
            }

            return await ConstruirToken(usuario);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            if (usuario is null)
            {
                return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });
            }

            var resultado = await signInManager.CheckPasswordSignInAsync(
                usuario, credencialesUsuario.Password, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(usuario);
            }

            if (resultado.IsLockedOut)
            {
                return Unauthorized(new { mensaje = "Usuario bloqueado temporalmente." });
            }

            return Unauthorized(new { mensaje = "Email o contraseña incorrectos." });
        }
    }
}
