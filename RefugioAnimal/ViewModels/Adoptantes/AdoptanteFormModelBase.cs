using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Web.ViewModels.Adoptante
{
    public abstract class AdoptanteFormModelBase
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre completo no puede superar los 150 caracteres.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; }

        [StringLength(30, ErrorMessage = "La identificación no puede superar los 30 caracteres.")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(30, ErrorMessage = "El teléfono no puede superar los 30 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres.")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [StringLength(250, ErrorMessage = "La dirección no puede superar los 250 caracteres.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar los 500 caracteres.")]
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
    }

    public sealed class EditarAdoptanteFormModel : AdoptanteFormModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }

    public sealed class CrearAdoptanteFormModel : AdoptanteFormModelBase
    {
    }
}