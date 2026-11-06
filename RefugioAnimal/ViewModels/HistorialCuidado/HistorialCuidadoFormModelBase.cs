using System;
using System.ComponentModel.DataAnnotations;
namespace RefugioAnimal.Web.ViewModels.HistorialCuidado
{
    public abstract class HistorialCuidadoFormModelBase
    {
        [Required(ErrorMessage = "Debe asociar el historial a una mascota.")]
        public Guid MascotaId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [Display(Name = "Fecha del Cuidado")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El tipo de cuidado es obligatorio.")]
        [StringLength(100, ErrorMessage = "El tipo no puede superar los 100 caracteres.")]
        public string Tipo { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El responsable es obligatorio.")]
        [StringLength(100, ErrorMessage = "El responsable no puede superar los 100 caracteres.")]
        public string Responsable { get; set; }

        [Display(Name = "Próxima Revisión")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProximaRevision { get; set; }
    }
}