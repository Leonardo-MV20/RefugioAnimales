using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.SolicitudAdopcion.Contratos
{
    public interface ISolicitudAdopcionService
    {
        Task<IEnumerable<SolicitudAdopcionDto>> GetAll();

        Task<SolicitudAdopcionDto> GetById(Guid id);

        Task<ValidationResult> Insert(SolicitudAdopcionRequest solicitud);

        Task<ValidationResult> Update(SolicitudAdopcionRequest solicitud);

        Task<ValidationResult> Delete(Guid id);
    }
}