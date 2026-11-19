using AutoMapper;
using RefugioAnimal.Application.Features.Adoptante;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Application.Features.SolicitudAdopcion;
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
                .ForMember(dest => dest.HistorialCuidados, src => src.Ignore())
                .ForMember(dest => dest.SolicitudesAdopcion, src => src.Ignore());

            CreateMap<HistorialCuidado, HistorialCuidadoDto>();

            CreateMap<HistorialCuidadoRequest, HistorialCuidado>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Mascota, src => src.Ignore());

            CreateMap<Adoptante, AdoptanteDto>();

            CreateMap<AdoptanteRequest, Adoptante>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.SolicitudesAdopcion, src => src.Ignore());

            CreateMap<SolicitudAdopcion, SolicitudAdopcionDto>()
                .ForMember(
                    dest => dest.Adoptante,
                    opt => opt.MapFrom(src => src.Adoptante.NombreCompleto))
                .ForMember(
                    dest => dest.Mascota,
                    opt => opt.MapFrom(src => src.Mascota.Nombre));

            CreateMap<SolicitudAdopcionRequest, SolicitudAdopcion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Adoptante, opt => opt.Ignore())
                .ForMember(dest => dest.Mascota, opt => opt.Ignore());
        }
    }
}