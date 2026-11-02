using System;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Domain.Entities
{
    public class HistorialCuidado
    {
        public Guid Id { get; set; }

        public Guid MascotaId { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(150)]
        public string Responsable { get; set; }

        public DateTime? ProximaRevision { get; set; }

        public virtual Mascota Mascota { get; set; }
    }
}