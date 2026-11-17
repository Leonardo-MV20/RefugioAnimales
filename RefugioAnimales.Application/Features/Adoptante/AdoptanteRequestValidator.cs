using FluentValidation;

namespace RefugioAnimal.Application.Features.Adoptante
{
    public class AdoptanteRequestValidator : AbstractValidator<AdoptanteRequest>
    {
        public AdoptanteRequestValidator()
        {
            RuleFor(x => x.NombreCompleto)
                .NotEmpty()
                .WithMessage("El nombre completo es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El nombre completo no puede superar los 150 caracteres.");

            RuleFor(x => x.Identificacion)
                .MaximumLength(30)
                .WithMessage("La identificación no puede superar los 30 caracteres.");

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage("El teléfono es obligatorio.")
                .MaximumLength(30)
                .WithMessage("El teléfono no puede superar los 30 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty()
                .WithMessage("El correo es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El correo no puede superar los 150 caracteres.");

            RuleFor(x => x.Direccion)
                .MaximumLength(250)
                .WithMessage("La dirección no puede superar los 250 caracteres.");

            RuleFor(x => x.Observaciones)
                .MaximumLength(500)
                .WithMessage("Las observaciones no pueden superar los 500 caracteres.");
        }
    }
}