using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.Adoptante.Contratos
{
    public interface IAdoptanteRepository
    {
        Task<IEnumerable<AdoptanteDto>> GetAll();

        Task<AdoptanteDto> GetById(Guid id);

        Task Insert(AdoptanteRequest adoptante);

        Task Update(AdoptanteRequest adoptante);

        Task Delete(Guid id);
    }
}