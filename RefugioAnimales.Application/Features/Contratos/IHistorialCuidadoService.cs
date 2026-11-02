using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace RefugioAnimal.Application.Features.HistorialCuidado.Contratos
{
    public interface IHistorialCuidadoService
    {
        Task<IEnumerable<HistorialCuidadoDto>> GetByMascota(Guid mascotaId);

        Task<HistorialCuidadoDto> GetById(Guid id);

        Task<ValidationResult> Insert(HistorialCuidadoRequest request);
    }
}