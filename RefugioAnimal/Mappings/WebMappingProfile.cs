using AutoMapper;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Web.ViewModels.Mascota;

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
        }
    }
}