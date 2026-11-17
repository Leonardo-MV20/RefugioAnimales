using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using FluentValidation;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Application.Features.HistorialCuidado.Contratos;
using RefugioAnimal.Application.Features.Adoptante;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using RefugioAnimal.Infrastructure.Data;
using RefugioAnimal.Infrastructure.Persistence.Mascotas;
using RefugioAnimal.Infrastructure.Persistence.HistorialCuidados;
using RefugioAnimal.Infrastructure.Persistence.Adoptantes;

namespace RefugioAnimal.Web.App_Start
{
    public static class ContainerConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<RefugioContext>()
                .InstancePerRequest();

            builder.RegisterInstance(AutoMapperConfig.Mapper)
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<MascotaRepository>()
                .As<IMascotaRepository>()
                .InstancePerRequest();

            builder.RegisterType<MascotaService>()
                .As<IMascotaService>()
                .InstancePerRequest();

            builder.RegisterType<MascotaRequestValidator>()
                .As<IValidator<MascotaRequest>>()
                .InstancePerRequest();

            builder.RegisterType<HistorialCuidadoRepository>()
                .As<IHistorialCuidadoRepository>()
                .InstancePerRequest();

            builder.RegisterType<HistorialCuidadoService>()
                .As<IHistorialCuidadoService>()
                .InstancePerRequest();

            builder.RegisterType<HistorialCuidadoRequestValidator>()
                .InstancePerRequest();

            builder.RegisterType<AdoptanteRepository>()
                .As<IAdoptanteRepository>()
                .InstancePerRequest();

            builder.RegisterType<AdoptanteService>()
                .As<IAdoptanteService>()
                .InstancePerRequest();

            builder.RegisterType<AdoptanteRequestValidator>()
                .As<IValidator<AdoptanteRequest>>()
                .InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(container));
        }
    }
}