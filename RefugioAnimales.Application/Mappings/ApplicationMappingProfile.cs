using AutoMapper;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Domain.Entities;

namespace RefugioAnimal.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Mascota, MascotaDto>();

            CreateMap<MascotaRequest, Mascota>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.HistorialCuidados, src => src.Ignore());

            CreateMap<HistorialCuidado, HistorialCuidadoDto>();

            CreateMap<HistorialCuidadoRequest, HistorialCuidado>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Mascota, src => src.Ignore());
        }
    }
}