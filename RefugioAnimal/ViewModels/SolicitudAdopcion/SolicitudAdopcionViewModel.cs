using System;

namespace RefugioAnimal.Web.ViewModels.SolicitudAdopcion
{
    public class SolicitudAdopcionViewModel
    {
        public Guid Id { get; set; }
        public Guid AdoptanteId { get; set; }
        public Guid MascotaId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Adoptante { get; set; }
        public string Mascota { get; set; }
    }
}