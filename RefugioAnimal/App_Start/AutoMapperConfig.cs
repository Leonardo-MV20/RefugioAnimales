using AutoMapper;
using RefugioAnimal.Application.Mappings;
using RefugioAnimal.Web.Mappings;

namespace RefugioAnimal.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Register()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMappingProfile>();
                cfg.AddProfile<WebMappingProfile>();
            });

            configuration.AssertConfigurationIsValid();

            Mapper = configuration.CreateMapper();
        }
    }
}