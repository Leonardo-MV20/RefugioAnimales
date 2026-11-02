using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Mascota.Contratos;

namespace RefugioAnimal.Application.Features.Mascota
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repository;
        private readonly MascotaRequestValidator _validator;

        public MascotaService(
            IMascotaRepository repository,
            MascotaRequestValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<MascotaDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<MascotaDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(MascotaRequest request)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
                return result;

            await _repository.Insert(request);

            return result;
        }

        public async Task<ValidationResult> Update(MascotaRequest request)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
                return result;

            var mascota = await _repository.GetById(request.Id);

            if (mascota == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "Id",
                        "La mascota no existe."));

                return result;
            }

            await _repository.Update(request);

            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var mascota = await _repository.GetById(id);

            if (mascota == null)
                return false;

            await _repository.Delete(id);

            return true;
        }
    }
}