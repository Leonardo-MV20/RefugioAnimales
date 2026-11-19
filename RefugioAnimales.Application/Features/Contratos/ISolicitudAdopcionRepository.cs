using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.SolicitudAdopcion.Contratos
{
    public interface ISolicitudAdopcionRepository
    {
        Task<IEnumerable<SolicitudAdopcionDto>> GetAll();
        Task<SolicitudAdopcionDto> GetById(Guid id);
        Task Insert(SolicitudAdopcionRequest solicitud);
        Task Update(SolicitudAdopcionRequest solicitud);
        Task Delete(Guid id);
    }
}