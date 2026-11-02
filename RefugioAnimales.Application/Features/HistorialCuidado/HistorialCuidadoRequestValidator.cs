using System;
using FluentValidation;

namespace RefugioAnimal.Application.Features.HistorialCuidado
{
    public class HistorialCuidadoRequestValidator : AbstractValidator<HistorialCuidadoRequest>
    {
        public HistorialCuidadoRequestValidator()
        {
            RuleFor(x => x.MascotaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Debe seleccionar una mascota.");

            RuleFor(x => x.Tipo)
                .NotEmpty()
                .WithMessage("El tipo de cuidado es obligatorio.")
                .MaximumLength(50)
                .WithMessage("El tipo de cuidado admite hasta 50 caracteres.");

            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripción es obligatoria.")
                .MaximumLength(500)
                .WithMessage("La descripción admite hasta 500 caracteres.");

            RuleFor(x => x.Responsable)
                .MaximumLength(150)
                .WithMessage("El responsable admite hasta 150 caracteres.");
        }
    }
}