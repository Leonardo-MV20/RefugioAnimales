using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RefugioAnimal.Application.Features.HistorialCuidado;
using RefugioAnimal.Application.Features.HistorialCuidado.Contratos;
using RefugioAnimal.Infrastructure.Data;

namespace RefugioAnimal.Infrastructure.Persistence.HistorialCuidados
{
    public class HistorialCuidadoRepository : IHistorialCuidadoRepository
    {
        private readonly RefugioContext _context;
        private readonly IMapper _mapper;

        public HistorialCuidadoRepository(
            RefugioContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HistorialCuidadoDto>> GetByMascota(Guid mascotaId)
        {
            var historiales = await _context.HistorialCuidados
                .Where(x => x.MascotaId == mascotaId)
                .OrderByDescending(x => x.Fecha)
                .ToListAsync();

            return _mapper.Map<IEnumerable<HistorialCuidadoDto>>(historiales);
        }

        public async Task<HistorialCuidadoDto> GetById(Guid id)
        {
            var historial = await _context.HistorialCuidados
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<HistorialCuidadoDto>(historial);
        }

        public async Task Insert(HistorialCuidadoRequest historial)
        {
            var entity =
                _mapper.Map<Domain.Entities.HistorialCuidado>(historial);

            entity.Id = Guid.NewGuid();

            _context.HistorialCuidados.Add(entity);

            await _context.SaveChangesAsync();
        }
    }
}