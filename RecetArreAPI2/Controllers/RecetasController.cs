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
        public async Task<ActionResult<IEnumerable<RecetasDto>>> GetRecetas()
        {
            var recetas = await context.Recetas
                .Include(r => r.IdTiempo)
                .Include(r => r.RecetasCategorias)
                .ToListAsync();

            return Ok(mapper.Map<List<RecetasDto>>(recetas));
        }

        // GET: api/recetas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RecetasDto>> GetReceta(int id)
        {
            var receta = await context.Recetas
                .Include(r => r.IdTiempo)
                .Include(r => r.RecetasCategorias)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (receta == null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            return Ok(mapper.Map<RecetasDto>(receta));
        }

        // POST: api/recetas
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RecetasDto>> CreateReceta(RecetasCreacionDto recetasCreacion)
        {
            // Validar que el nombre no esté duplicado
            var existe = await context.Recetas
                .AnyAsync(c => c.Nombre.ToLower() == recetasCreacion.Nombre.ToLower());

            if (existe)
            {
                return BadRequest(new { mensaje = "Ya existe esa receta" });
            }

            // Obtener el usuario autenticado
            var usuarioId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado" });
            }

            var receta = mapper.Map<Receta>(recetasCreacion);
            receta.IdUsuario = usuarioId;

            var tiemposIds = recetasCreacion.TiemposIds.Distinct().ToList();
            if (tiemposIds.Count > 0)
            {
                var tiemposExistentes = await context.Tiempos
                    .Where(t => tiemposIds.Contains(t.Id))
                    .Select(t => t.Id)
                    .ToListAsync();

                if (tiemposExistentes.Count != tiemposIds.Count)
                {
                    return BadRequest(new { mensaje = "Uno o más Ids de tiempo no existen." });
                }
            }

            var categoriasIds = recetasCreacion.CategoriasIds.Distinct().ToList();
            if (categoriasIds.Count > 0)
            {
                var categoriasExistentes = await context.Categorias
                    .Where(c => categoriasIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (categoriasExistentes.Count != categoriasIds.Count)
                {
                    return BadRequest(new { mensaje = "Uno o más Ids de categoría no existen." });
                }
            }

            context.Recetas.Add(receta);
            await context.SaveChangesAsync();

            if (tiemposIds.Count > 0)
            {
                var relaciones = tiemposIds.Select(tiempoId => new Rec_Tiem
                {
                    IdReceta = receta.Id,
                    IdTiempo = tiempoId
                });

                context.RecetasTiempos.AddRange(relaciones);
            }

            if (categoriasIds.Count > 0)
            {
                var relacionesCategorias = categoriasIds.Select(categoriaId => new Rec_Cat
                {
                    IdReceta = receta.Id,
                    IdCategoria = categoriaId
                });

                context.RecetasCategorias.AddRange(relacionesCategorias);
            }

            await context.SaveChangesAsync();

            receta = await context.Recetas
                .Include(r => r.IdTiempo)
                .Include(r => r.RecetasCategorias)
                .FirstAsync(r => r.Id == receta.Id);

            return CreatedAtAction(nameof(GetReceta), new { id = receta.Id }, mapper.Map<RecetasDto>(receta));
        }

        // PUT: api/recetas/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateReceta(int id, RecetasModificacionDto recetasModificacion)
        {
            var receta = await context.Recetas
                .Include(r => r.IdTiempo)
                .Include(r => r.RecetasCategorias)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (receta == null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            // Validar que el nombre no esté duplicado (si cambió)
            if (!receta.Nombre.Equals(recetasModificacion.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await context.Recetas
                    .AnyAsync(c => c.Nombre.ToLower() == recetasModificacion.Nombre.ToLower() && c.Id != id);

                if (existe)
                {
                    return BadRequest(new { mensaje = "Ya existe esa receta." });
                }
            }

            mapper.Map(recetasModificacion, receta);

            var tiemposIds = recetasModificacion.TiemposIds.Distinct().ToList();
            if (tiemposIds.Count > 0)
            {
                var existentes = await context.Tiempos
                    .Where(t => tiemposIds.Contains(t.Id))
                    .Select(t => t.Id)
                    .ToListAsync();

                if (existentes.Count != tiemposIds.Count)
                {
                    return BadRequest(new { mensaje = "Uno o más Ids de tiempo no existen." });
                }
            }

            var categoriasIds = recetasModificacion.CategoriasIds.Distinct().ToList();
            if (categoriasIds.Count > 0)
            {
                var categoriasExistentes = await context.Categorias
                    .Where(c => categoriasIds.Contains(c.Id))
                    .Select(c => c.Id)
                    .ToListAsync();

                if (categoriasExistentes.Count != categoriasIds.Count)
                {
                    return BadRequest(new { mensaje = "Uno o más Ids de categoría no existen." });
                }
            }

            context.RecetasTiempos.RemoveRange(receta.IdTiempo);
            context.RecetasCategorias.RemoveRange(receta.RecetasCategorias);

            if (tiemposIds.Count > 0)
            {
                var nuevasRelaciones = tiemposIds.Select(tiempoId => new Rec_Tiem
                {
                    IdReceta = receta.Id,
                    IdTiempo = tiempoId
                });

                context.RecetasTiempos.AddRange(nuevasRelaciones);
            }

            if (categoriasIds.Count > 0)
            {
                var nuevasRelacionesCategorias = categoriasIds.Select(categoriaId => new Rec_Cat
                {
                    IdReceta = receta.Id,
                    IdCategoria = categoriaId
                });

                context.RecetasCategorias.AddRange(nuevasRelacionesCategorias);
            }

            context.Recetas.Update(receta);
            await context.SaveChangesAsync();

            receta = await context.Recetas
                .Include(r => r.IdTiempo)
                .Include(r => r.RecetasCategorias)
                .FirstAsync(r => r.Id == receta.Id);

            return Ok(new { mensaje = "Receta actualizada exitosamente", data = mapper.Map<RecetasDto>(receta) });
        }

        // DELETE: api/recetas/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await context.Recetas.FirstOrDefaultAsync(c => c.Id == id);

            if (receta == null)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            context.Recetas.Remove(receta);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Receta eliminada exitosamente" });
        }
    }
}
