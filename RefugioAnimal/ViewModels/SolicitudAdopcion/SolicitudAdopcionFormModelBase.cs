using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RefugioAnimal.Web.ViewModels.SolicitudAdopcion
{
    public abstract class SolicitudAdopcionFormModelBase
    {
        [Required(ErrorMessage = "Debe seleccionar un adoptante.")]
        [Display(Name = "Adoptante")]
        public Guid AdoptanteId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una mascota.")]
        [Display(Name = "Mascota")]
        public Guid MascotaId { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es obligatoria.")]
        [Display(Name = "Fecha de solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(500, ErrorMessage = "El motivo no puede superar los 500 caracteres.")]
        [Display(Name = "Motivo")]
        public string Motivo { get; set; }

        public IEnumerable<SelectListItem> AdoptanteOptions { get; set; }

        public IEnumerable<SelectListItem> MascotaOptions { get; set; }
    }

    public sealed class EditarSolicitudAdopcionFormModel
        : SolicitudAdopcionFormModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }

    public sealed class CrearSolicitudAdopcionFormModel
        : SolicitudAdopcionFormModelBase
    {
    }
}