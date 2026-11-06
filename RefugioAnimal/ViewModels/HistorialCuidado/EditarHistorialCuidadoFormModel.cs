using RefugioAnimal.Web.ViewModels.HistorialCuidado;
using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Web.ViewModels.HistorialCuidado
{
    public class EditarHistorialCuidadoFormModel : HistorialCuidadoFormModelBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}