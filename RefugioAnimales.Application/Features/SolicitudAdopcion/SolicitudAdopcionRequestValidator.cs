using FluentValidation;
using System;

namespace RefugioAnimal.Application.Features.SolicitudAdopcion
{
    public class SolicitudAdopcionRequestValidator
        : AbstractValidator<SolicitudAdopcionRequest>
    {
        public SolicitudAdopcionRequestValidator()
        {
            RuleFor(x => x.AdoptanteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Debe seleccionar un adoptante.");

            RuleFor(x => x.MascotaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Debe seleccionar una mascota.");

            RuleFor(x => x.FechaSolicitud)
                .NotEmpty()
                .WithMessage("La fecha de solicitud es obligatoria.");

            RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage("El estado es obligatorio.")
                .MaximumLength(30)
                .WithMessage("El estado no puede superar los 30 caracteres.");

            RuleFor(x => x.Motivo)
                .NotEmpty()
                .WithMessage("El motivo es obligatorio.")
                .MaximumLength(500)
                .WithMessage("El motivo no puede superar los 500 caracteres.");
        }
    }
}