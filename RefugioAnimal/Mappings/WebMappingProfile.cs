using AutoMapper;
using RefugioAnimal.Application.Features.Adoptante;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Web.ViewModels.Adoptante;
using RefugioAnimal.Web.ViewModels.Mascota;
using RefugioAnimal.Web.ViewModels.HistorialCuidado;

namespace RefugioAnimal.Web.Mappings
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<MascotaDto, MascotaViewModel>();

            CreateMap<MascotaDto, EditarMascotaFormModel>();

            CreateMap<EditarMascotaFormModel, MascotaRequest>();

            CreateMap<CrearMascotaFormModel, MascotaRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<HistorialCuidadoDto, HistorialCuidadoViewModel>();

            CreateMap<CrearHistorialCuidadoFormModel, HistorialCuidadoRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EditarHistorialCuidadoFormModel, HistorialCuidadoRequest>();

            CreateMap<HistorialCuidadoDto, EditarHistorialCuidadoFormModel>();

            CreateMap<AdoptanteDto, AdoptanteViewModel>();

            CreateMap<AdoptanteDto, EditarAdoptanteFormModel>();

            CreateMap<EditarAdoptanteFormModel, AdoptanteRequest>();

            CreateMap<CrearAdoptanteFormModel, AdoptanteRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}