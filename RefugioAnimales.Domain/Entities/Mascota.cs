using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Domain.Entities
{
    public class Mascota
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Especie { get; set; }

        [StringLength(100)]
        public string Raza { get; set; }

        public int? EdadAproximada { get; set; }

        [Required]
        [StringLength(20)]
        public string Sexo { get; set; }

        [Required]
        [StringLength(30)]
        public string Estado { get; set; }

        public DateTime FechaIngreso { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public bool EstaActivo { get; set; }

        public virtual ICollection<HistorialCuidado> HistorialCuidados { get; set; }
            = new List<HistorialCuidado>();
    }
}