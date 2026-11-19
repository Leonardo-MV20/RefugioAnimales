using FluentValidation;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using RefugioAnimal.Application.Features.SolicitudAdopcion.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefugioAnimal.Application.Features.SolicitudAdopcion
{
    public class SolicitudAdopcionService : ISolicitudAdopcionService
    {
        private readonly ISolicitudAdopcionRepository _repository;
        private readonly IAdoptanteRepository _adoptanteRepository;
        private readonly IMascotaRepository _mascotaRepository;
        private readonly IValidator<SolicitudAdopcionRequest> _validator;

        public SolicitudAdopcionService(
            ISolicitudAdopcionRepository repository,
            IAdoptanteRepository adoptanteRepository,
            IMascotaRepository mascotaRepository,
            IValidator<SolicitudAdopcionRequest> validator)
        {
            _repository = repository;
            _adoptanteRepository = adoptanteRepository;
            _mascotaRepository = mascotaRepository;
            _validator = validator;
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var solicitud = await GetById(id);

            if (solicitud == null)
            {
                var validationResult = new ValidationResult();

                validationResult.Errors.Add(
                    new ValidationFailure(
                        "Id",
                        "La solicitud de adopción no existe."));

                return validationResult;
            }

            await _repository.Delete(id);

            return new ValidationResult();
        }

        public async Task<IEnumerable<SolicitudAdopcionDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SolicitudAdopcionDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(
            SolicitudAdopcionRequest solicitud)
            => await Save(solicitud, false);

        public async Task<ValidationResult> Update(
            SolicitudAdopcionRequest solicitud)
            => await Save(solicitud, true);

        private async Task<ValidationResult> Save(
            SolicitudAdopcionRequest solicitud,
            bool isUpdate)
        {
            var result = await _validator.ValidateAsync(solicitud);

            if (!result.IsValid)
            {
                return result;
            }

            if (await _adoptanteRepository.GetById(solicitud.AdoptanteId) == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "AdoptanteId",
                        "El adoptante seleccionado no existe."));

                return result;
            }

            if (await _mascotaRepository.GetById(solicitud.MascotaId) == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "MascotaId",
                        "La mascota seleccionada no existe."));

                return result;
            }

            if (isUpdate &&
                await _repository.GetById(solicitud.Id) == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "Id",
                        "La solicitud que intenta actualizar no existe."));

                return result;
            }

            await (
                isUpdate
                    ? _repository.Update(solicitud)
                    : _repository.Insert(solicitud)
            );

            return result;
        }
    }
}