using FluentValidation;

namespace RefugioAnimal.Application.Features.Mascota
{
    public class MascotaRequestValidator : AbstractValidator<MascotaRequest>
    {
        public MascotaRequestValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre de la mascota es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre admite hasta 100 caracteres.");

            RuleFor(x => x.Especie)
                .NotEmpty().WithMessage("La especie es obligatoria.")
                .MaximumLength(50).WithMessage("La especie admite hasta 50 caracteres.");

            RuleFor(x => x.Raza)
                .MaximumLength(100).WithMessage("La raza admite hasta 100 caracteres.");

            RuleFor(x => x.Sexo)
                .NotEmpty().WithMessage("El sexo es obligatorio.")
                .MaximumLength(20).WithMessage("El sexo admite hasta 20 caracteres.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .MaximumLength(30).WithMessage("El estado admite hasta 30 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(500).WithMessage("La descripción admite hasta 500 caracteres.");
        }
    }
}