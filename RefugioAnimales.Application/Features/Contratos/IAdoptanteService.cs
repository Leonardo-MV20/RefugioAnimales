using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.Adoptante.Contratos
{
    public interface IAdoptanteService
    {
        Task<IEnumerable<AdoptanteDto>> GetAll();

        Task<AdoptanteDto> GetById(Guid id);

        Task<ValidationResult> Insert(AdoptanteRequest adoptante);

        Task<ValidationResult> Update(AdoptanteRequest adoptante);

        Task<ValidationResult> Delete(Guid id);
    }
}