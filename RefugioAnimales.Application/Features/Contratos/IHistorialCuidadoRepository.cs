using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.HistorialCuidado.Contratos
{
    public interface IHistorialCuidadoRepository
    {
        Task<IEnumerable<HistorialCuidadoDto>> GetByMascota(Guid mascotaId);

        Task<HistorialCuidadoDto> GetById(Guid id);

        Task Insert(HistorialCuidadoRequest historial);
    }
}