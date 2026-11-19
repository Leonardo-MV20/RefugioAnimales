using System;

namespace RefugioAnimal.Application.Features.SolicitudAdopcion
{
    public class SolicitudAdopcionRequest
    {
        public Guid Id { get; set; }
        public Guid AdoptanteId { get; set; }
        public Guid MascotaId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
    }
}