using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Recetas;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RecetasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        // GET: api/recetas/
        [HttpGet]
        public async Task<ActionResult<RecetaDto>> GetRecetas(int id)
        {
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Instrucciones)
                .OrderByDescending(r => r.CreadoUtc)
                .ToListAsync();
            return Ok(mapper.Map<List<RecetaDto>>(recetas));
        }

        [HttpGet("filtrar/categorias")]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> FiltrarPorCategoria([FromQuery] List<int> categoriaIds)
        {
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Instrucciones)
                .Where(r => r.Categorias.Any(c => categoriaIds.Contains(c.Id)))
                .OrderByDescending(r => r.CreadoUtc)
                .ToListAsync();
            return Ok(mapper.Map<List<RecetaDto>>(recetas));
        }
    }
}
