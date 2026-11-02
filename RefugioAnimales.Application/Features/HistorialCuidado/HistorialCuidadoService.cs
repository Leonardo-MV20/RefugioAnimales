using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using RefugioAnimal.Application.Features.HistorialCuidado.Contratos;
using RefugioAnimal.Application.Features.Mascota.Contratos;

namespace RefugioAnimal.Application.Features.HistorialCuidado
{
    public class HistorialCuidadoService : IHistorialCuidadoService
    {
        private readonly IHistorialCuidadoRepository _repository;
        private readonly IMascotaRepository _mascotaRepository;
        private readonly HistorialCuidadoRequestValidator _validator;

        public HistorialCuidadoService(
            IHistorialCuidadoRepository repository,
            IMascotaRepository mascotaRepository,
            HistorialCuidadoRequestValidator validator)
        {
            _repository = repository;
            _mascotaRepository = mascotaRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<HistorialCuidadoDto>> GetByMascota(Guid mascotaId)
        {
            return await _repository.GetByMascota(mascotaId);
        }

        public async Task<HistorialCuidadoDto> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ValidationResult> Insert(HistorialCuidadoRequest request)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
                return result;

            var mascota = await _mascotaRepository.GetById(request.MascotaId);

            if (mascota == null)
            {
                result.Errors.Add(
                    new ValidationFailure(
                        "MascotaId",
                        "La mascota seleccionada no existe."));

                return result;
            }

            await _repository.Insert(request);

            return result;
        }
    }
}