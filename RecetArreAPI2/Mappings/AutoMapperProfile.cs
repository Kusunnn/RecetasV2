using AutoMapper;
using RecetArreAPI2.DTOs;
using RecetArreAPI2.DTOs.Categorias;
using RecetArreAPI2.DTOs.Comentarios;
using RecetArreAPI2.DTOs.Ingredientes;
using RecetArreAPI2.DTOs.Recetas;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ApplicationUser <-> ApplicationUserDto
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

            // Categoria mappings
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaCreacionDto, Categoria>();
            CreateMap<CategoriaModificacionDto, Categoria>();

            // Ingrediente mappings
            CreateMap<Ingrediente, IngredienteDto>();
            CreateMap<IngredienteCreacionDto, Ingrediente>();
            CreateMap<IngredienteModificacionDto, Ingrediente>();

            // Receta mappings
            CreateMap<Receta, RecetasDto>()
                .ForMember(dest => dest.TiemposIds,
                    opt => opt.MapFrom(src => src.IdTiempo.Select(x => x.IdTiempo)))
                .ForMember(dest => dest.CategoriasIds,
                    opt => opt.MapFrom(src => src.RecetasCategorias.Select(x => x.IdCategoria)));

            CreateMap<RecetasCreacionDto, Receta>()
                .ForMember(dest => dest.IdTiempo, opt => opt.Ignore())
                .ForMember(dest => dest.RecetasCategorias, opt => opt.Ignore());

            CreateMap<RecetasModificacionDto, Receta>()
                .ForMember(dest => dest.IdTiempo, opt => opt.Ignore())
                .ForMember(dest => dest.RecetasCategorias, opt => opt.Ignore());

            // Comentario mappings
            CreateMap<Comentario, ComentariosDto>();
            CreateMap<ComentariosCreacionDto, Comentario>();
            CreateMap<ComentariosModificacionDto, Comentario>();
        }
    }
}
