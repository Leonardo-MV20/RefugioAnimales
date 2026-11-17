using System;

namespace RefugioAnimal.Application.Features.Adoptante
{
    public class AdoptanteRequest
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Observaciones { get; set; }
    }
}