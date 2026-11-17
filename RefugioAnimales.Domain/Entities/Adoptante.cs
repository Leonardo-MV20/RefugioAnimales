using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefugioAnimal.Domain.Entities
{
    public class Adoptante
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreCompleto { get; set; }

        [StringLength(30)]
        public string Identificacion { get; set; }

        [Required]
        [StringLength(30)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(150)]
        public string Correo { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }
    }
}