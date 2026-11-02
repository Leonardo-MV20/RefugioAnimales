using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.Mascota.Contratos
{
    public interface IMascotaRepository
    {
        Task<IEnumerable<MascotaDto>> GetAll();

        Task<MascotaDto> GetById(Guid id);

        Task Insert(MascotaRequest mascota);

        Task Update(MascotaRequest mascota);

        Task Delete(Guid id);
    }
}