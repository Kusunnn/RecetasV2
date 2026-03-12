using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Recetas;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RecetasController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        // GET: api/recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> GetRecetas()
        {
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Ingredientes)
                .OrderByDescending(r => r.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<RecetaDto>>(recetas));
        }

        // GET: api/recetas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecetaDto>> GetReceta(int id)
        {
            var receta = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receta is null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            return Ok(mapper.Map<RecetaDto>(receta));
        }

        [HttpGet("filtrar/categorias")]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> FiltrarPorCategoria([FromQuery] List<int> categoriaIds)
        {
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Ingredientes)
                .Where(r => r.Categorias.Any(c => categoriaIds.Contains(c.Id)))
                .OrderByDescending(r => r.CreadoUtc)
                .ToListAsync();
            return Ok(mapper.Map<List<RecetaDto>>(recetas));
        }

        // POST: api/recetas
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RecetaDto>> CreateReceta(RecetaCreacionDto recetaCreacionDto)
        {
            var usuarioId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado" });
            }

            var receta = mapper.Map<Recetas>(recetaCreacionDto);
            receta.AutorId = usuarioId;
            receta.CreadoUtc = DateTime.UtcNow;
            receta.ModificadoUtc = DateTime.UtcNow;

            context.Recetas.Add(receta);
            await context.SaveChangesAsync();

            var recetaCreada = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Ingredientes)
                .FirstAsync(r => r.Id == receta.Id);

            return CreatedAtAction(nameof(GetReceta), new { id = receta.Id }, mapper.Map<RecetaDto>(recetaCreada));
        }

        // PUT: api/recetas/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateReceta(int id, RecetaModificacionDto recetaModificacionDto)
        {
            var receta = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receta is null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            mapper.Map(recetaModificacionDto, receta);
            receta.ModificadoUtc = DateTime.UtcNow;

            context.Recetas.Update(receta);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Receta actualizada exitosamente", data = mapper.Map<RecetaDto>(receta) });
        }

        // DELETE: api/recetas/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await context.Recetas.FirstOrDefaultAsync(r => r.Id == id);
            if (receta is null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            context.Recetas.Remove(receta);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Receta eliminada exitosamente" });
        }
    }
}
