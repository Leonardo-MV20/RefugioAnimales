using AutoMapper;
using RefugioAnimal.Application.Features.Adoptante;
using RefugioAnimal.Application.Features.Adoptante.Contratos;
using RefugioAnimal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RefugioAnimal.Infrastructure.Persistence.Adoptantes
{
    public class AdoptanteRepository : IAdoptanteRepository
    {
        private readonly RefugioContext _context;
        private readonly IMapper _mapper;

        public AdoptanteRepository(
            RefugioContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdoptanteDto>> GetAll()
        {
            var adoptantes = await _context.Adoptantes
                .ToListAsync();

            return _mapper.Map<IEnumerable<AdoptanteDto>>(adoptantes);
        }

        public async Task<AdoptanteDto> GetById(Guid id)
        {
            var adoptante = await _context.Adoptantes
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<AdoptanteDto>(adoptante);
        }

        public async Task Insert(AdoptanteRequest adoptante)
        {
            var entity = _mapper.Map<Domain.Entities.Adoptante>(adoptante);

            entity.Id = Guid.NewGuid();

            _context.Adoptantes.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(AdoptanteRequest adoptante)
        {
            var entity = await _context.Adoptantes
                .FirstOrDefaultAsync(x => x.Id == adoptante.Id);

            _mapper.Map(adoptante, entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Adoptantes
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Adoptantes.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}