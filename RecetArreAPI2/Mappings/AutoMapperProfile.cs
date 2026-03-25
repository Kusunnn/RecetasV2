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
            CreateMap<Recetas, RecetaDto>()
                .ForMember(dest => dest.CategoriaIds, opt => opt.MapFrom(src => src.Categorias.Select(c => c.Id)))
                .ForMember(dest => dest.IngredienteIds, opt => opt.MapFrom(src => src.Ingredientes.Select(i => i.Id)));


            CreateMap<RecetaCreacionDto, Recetas>();
            CreateMap<RecetaModificacionDto, Recetas>();

            // Comentario mappings
            CreateMap<Comentario, ComentarioDto>()
                .ForMember(dest => dest.UsuarioNombre,
                    opt => opt.MapFrom(src => src.Usuario.UserName ?? src.Usuario.Email ?? src.UsuarioId));
            CreateMap<ComentarioCreacionDto, Comentario>();
            CreateMap<ComentarioModificacionDto, Comentario>();
        }
    }
}
