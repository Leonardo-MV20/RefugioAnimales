using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Web.ViewModels.Mascota
{
    public abstract class MascotaFormModelBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria.")]
        [StringLength(50, ErrorMessage = "La especie no puede superar los 50 caracteres.")]
        [Display(Name = "Especie")]
        public string Especie { get; set; }

        [StringLength(100, ErrorMessage = "La raza no puede superar los 100 caracteres.")]
        [Display(Name = "Raza")]
        public string Raza { get; set; }

        [Display(Name = "Edad aproximada")]
        public int? EdadAproximada { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio.")]
        [StringLength(20, ErrorMessage = "El sexo no puede superar los 20 caracteres.")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "¿Está activo?")]
        public bool EstaActivo { get; set; }
    }

    public sealed class EditarMascotaFormModel : MascotaFormModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }

    public sealed class CrearMascotaFormModel : MascotaFormModelBase
    {

    }
}