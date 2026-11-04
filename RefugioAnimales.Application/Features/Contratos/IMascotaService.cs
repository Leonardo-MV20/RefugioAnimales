using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace RefugioAnimal.Application.Features.Mascota.Contratos
{
    public interface IMascotaService
    {
        Task<IEnumerable<MascotaDto>> GetAll();

        Task<MascotaDto> GetById(Guid id);

        Task<ValidationResult> Insert(MascotaRequest request);

        Task<ValidationResult> Update(MascotaRequest request);

        Task<ValidationResult> Delete(Guid id);
    }
}