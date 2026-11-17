using FluentValidation;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.Adoptante
{
    public class AdoptanteService : IAdoptanteService
    {
        private readonly IAdoptanteRepository _repository;
        private readonly IValidator<AdoptanteRequest> _validator;

        public AdoptanteService(
            IAdoptanteRepository repository,
            IValidator<AdoptanteRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var adoptante = await GetById(id);

            if (adoptante == null)
            {
                var validationResult = new ValidationResult();

                validationResult.Errors.Add(
                    new ValidationFailure(
                        "Id",
                        "El adoptante no existe."));

                return validationResult;
            }

            await _repository.Delete(id);

            return new ValidationResult();
        }

        public async Task<IEnumerable<AdoptanteDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<AdoptanteDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(AdoptanteRequest adoptante)
            => await Save(adoptante, false);

        public async Task<ValidationResult> Update(AdoptanteRequest adoptante)
            => await Save(adoptante, true);

        private async Task<ValidationResult> Save(
            AdoptanteRequest adoptante,
            bool isUpdate)
        {
            var result = await _validator.ValidateAsync(adoptante);

            if (!result.IsValid)
            {
                return result;
            }

            if (isUpdate &&
                await _repository.GetById(adoptante.Id) == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "Id",
                        "El adoptante que intenta actualizar no existe."));

                return result;
            }

            await (
                isUpdate
                    ? _repository.Update(adoptante)
                    : _repository.Insert(adoptante)
            );

            return result;
        }
    }
}
