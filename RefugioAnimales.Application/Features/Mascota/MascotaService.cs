using FluentValidation;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.Mascota
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repository;
        private readonly IValidator<MascotaRequest> _validator;

        public MascotaService(
            IMascotaRepository repository,
            IValidator<MascotaRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var mascota = await GetById(id);
            if (mascota == null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("Id", "La mascota no existe."));
                return validationResult;
            }

            await _repository.Delete(id);
            return new ValidationResult();
        }

        public async Task<IEnumerable<MascotaDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<MascotaDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(MascotaRequest mascota)
            => await Save(mascota, false);

        public async Task<ValidationResult> Update(MascotaRequest mascota)
            => await Save(mascota, true);

        private async Task<ValidationResult> Save(MascotaRequest mascota, bool isUpdate)
        {
            var result = await _validator.ValidateAsync(mascota);
            if (!result.IsValid)
            {
                return result;
            }

            if (isUpdate && await _repository.GetById(mascota.Id) == null)
            {
                result.Errors.Add(new ValidationFailure("Id", "La mascota que intenta actualizar no existe."));
                return result;
            }

            await (
                isUpdate
                    ? _repository.Update(mascota)
                    : _repository.Insert(mascota)
            );

            return result;
        }
    }
}