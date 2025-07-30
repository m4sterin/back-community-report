using AutoMapper;
using back_community_report.DAL.DTO;
using back_community_report.DAL.Models;

namespace back_community_report
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            // Usuarios Publicos
            CreateMap<UsuarioPublico, UsuarioPublicoDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<UsuarioPublicoDto, UsuarioPublico>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Usuarios Publicos
            CreateMap<UsuarioRestrito, UsuarioRestritoDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<UsuarioRestritoDto, UsuarioRestrito>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Chamados
            CreateMap<Chamado, ChamadoDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<ChamadoDto, Chamado>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Historicos
            CreateMap<Historico, HistoricoDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<HistoricoDto, Historico>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Prefeituras
            CreateMap<Prefeitura, PrefeituraDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<PrefeituraDto, Prefeitura>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Setores
            CreateMap<Setor, SetorDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<SetorDto, Setor>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Categorias
            CreateMap<Categoria, CategoriaDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<CategoriaDto, Categoria>().
                AfterMap((dto, model) => model.Id = dto.Id);

            // Tipo
            CreateMap<Tipo, TipoDto>().
                AfterMap((model, dto) => dto.Id = model.Id);
            
            CreateMap<TipoDto, Tipo>().
                AfterMap((dto, model) => model.Id = dto.Id);
        }
    }
}