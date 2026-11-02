using System;

namespace RefugioAnimal.Application.Features.HistorialCuidado
{
    public class HistorialCuidadoDto
    {
        public Guid Id { get; set; }
        public Guid MascotaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Responsable { get; set; }
        public DateTime? ProximaRevision { get; set; }
    }
}