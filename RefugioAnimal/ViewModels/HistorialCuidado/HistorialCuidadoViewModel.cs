using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Web.ViewModels.HistorialCuidado
{
    public class HistorialCuidadoViewModel
    {
        public Guid Id { get; set; }
        public Guid MascotaId { get; set; }

        [Display(Name = "Fecha del Cuidado")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        public string Tipo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public string Responsable { get; set; }

        [Display(Name = "Próxima Revisión")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ProximaRevision { get; set; }
    }
}