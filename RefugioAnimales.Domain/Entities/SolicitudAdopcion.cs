using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Domain.Entities
{
    public class SolicitudAdopcion
    {
        public Guid Id { get; set; }

        public Guid AdoptanteId { get; set; }

        public Guid MascotaId { get; set; }

        public DateTime FechaSolicitud { get; set; }

        [Required]
        [StringLength(30)]
        public string Estado { get; set; }

        [Required]
        [StringLength(500)]
        public string Motivo { get; set; }

        public Adoptante Adoptante { get; set; }

        public Mascota Mascota { get; set; }
    }
}