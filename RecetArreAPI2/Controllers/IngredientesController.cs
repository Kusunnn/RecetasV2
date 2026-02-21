using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Ingredientes;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public IngredientesController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/ingredientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDto>>> GetIngredientes()
        {
            var ingredientes = await context.Ingredientes
                .ToListAsync();

            return Ok(mapper.Map<List<IngredienteDto>>(ingredientes));
        }

        // GET: api/ingredientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredienteDto>> GetIngrediente(int id)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingrediente == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            return Ok(mapper.Map<IngredienteDto>(ingrediente));
        }

        // POST: api/ingrediente
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IngredienteDto>> CreateIngrediente(IngredienteCreacionDto ingredienteCreacionDto)
        {
            // Validar que el nombre no esté duplicado
            var existe = await context.Ingredientes
                .AnyAsync(c => c.Nombre.ToLower() == ingredienteCreacionDto.Nombre.ToLower());

            if (existe)
            {
                return BadRequest(new { mensaje = "Ya existe ese ingrediente" });
            }

            // Obtener el usuario autenticado
            var usuarioId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado" });
            }

            var ingrediente = mapper.Map<Ingrediente>(ingredienteCreacionDto);

            context.Ingredientes.Add(ingrediente);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngrediente), new { id = ingrediente.Id }, mapper.Map<IngredienteDto>(ingrediente));
        }

        // PUT: api/ingrediente/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateIngrediente(int id, IngredienteModificacionDto ingredienteModificacionDto)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingrediente == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            // Validar que el nombre no esté duplicado (si cambió)
            if (!ingrediente.Nombre.Equals(ingredienteModificacionDto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await context.Ingredientes
                    .AnyAsync(c => c.Nombre.ToLower() == ingredienteModificacionDto.Nombre.ToLower() && c.Id != id);

                if (existe)
                {
                    return BadRequest(new { mensaje = "Ya existe ese ingrediente." });
                }
            }

            mapper.Map(ingredienteModificacionDto, ingrediente);
            context.Ingredientes.Update(ingrediente);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Ingrediente actualizado exitosamente", data = mapper.Map<IngredienteDto>(ingrediente) });
        }

        // DELETE: api/ingrediente/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteIngrediente(int id)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingrediente == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            context.Ingredientes.Remove(ingrediente);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Ingrediente eliminado exitosamente" });
        }
    }
}
