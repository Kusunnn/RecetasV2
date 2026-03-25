using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Comentarios;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public ComentariosController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentarios()
        {
            var comentarios = await context.Comentarios
                .Include(c => c.Usuario)
                .OrderByDescending(c => c.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<ComentarioDto>>(comentarios));
        }

        // GET: api/comentarios/receta/{recetaId}
        [HttpGet("receta/{recetaId:int}")]
        public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentariosPorReceta(int recetaId)
        {
            var existeReceta = await context.Recetas.AnyAsync(r => r.Id == recetaId);
            if (!existeReceta)
            {
                return NotFound(new { mensaje = "Receta no encontrada" });
            }

            var comentarios = await context.Comentarios
                .Include(c => c.Usuario)
                .Where(c => c.RecetaId == recetaId)
                .OrderByDescending(c => c.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<ComentarioDto>>(comentarios));
        }

        // GET: api/comentarios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioDto>> GetComentario(int id)
        {
            var comentario = await context.Comentarios
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comentario == null)
            {
                return NotFound(new { mensaje = "Comentario no encontrado" });
            }

            return Ok(mapper.Map<ComentarioDto>(comentario));
        }

        // POST: api/comentarios
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ComentarioDto>> CreateComentario(ComentarioCreacionDto comentarioCreacionDto)
        {
            var existeReceta = await context.Recetas.AnyAsync(r => r.Id == comentarioCreacionDto.RecetaId);
            if (!existeReceta)
            {
                return BadRequest(new { mensaje = "La receta indicada no existe" });
            }

            var usuarioId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado" });
            }

            var comentario = mapper.Map<Comentario>(comentarioCreacionDto);
            comentario.UsuarioId = usuarioId;
            comentario.CreadoUtc = DateTime.UtcNow;

            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();

            var comentarioCreado = await context.Comentarios
                .Include(c => c.Usuario)
                .FirstAsync(c => c.Id == comentario.Id);

            return CreatedAtAction(nameof(GetComentario), new { id = comentario.Id }, mapper.Map<ComentarioDto>(comentarioCreado));
        }

        // PUT: api/comentarios/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateComentario(int id, ComentarioModificacionDto comentarioModificacionDto)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if (comentario == null)
            {
                return NotFound(new { mensaje = "Comentario no encontrado" });
            }

            var existeReceta = await context.Recetas.AnyAsync(r => r.Id == comentarioModificacionDto.RecetaId);
            if (!existeReceta)
            {
                return BadRequest(new { mensaje = "La receta indicada no existe" });
            }

            mapper.Map(comentarioModificacionDto, comentario);
            context.Comentarios.Update(comentario);
            await context.SaveChangesAsync();

            await context.Entry(comentario).Reference(c => c.Usuario).LoadAsync();

            return Ok(new { mensaje = "Comentario actualizado exitosamente", data = mapper.Map<ComentarioDto>(comentario) });
        }

        // DELETE: api/comentarios/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(c => c.Id == id);
            if (comentario == null)
            {
                return NotFound(new { mensaje = "Comentario no encontrado" });
            }

            context.Comentarios.Remove(comentario);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Comentario eliminado exitosamente" });
        }
    }
}
