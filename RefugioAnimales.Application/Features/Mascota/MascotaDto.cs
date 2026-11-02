using System;

namespace RefugioAnimal.Application.Features.Mascota
{
    public class MascotaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int? EdadAproximada { get; set; }
        public string Sexo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public bool EstaActivo { get; set; }
    }
}