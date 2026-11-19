using AutoMapper;
using RefugioAnimal.Application.Features.SolicitudAdopcion;
using RefugioAnimal.Application.Features.SolicitudAdopcion.Contratos;
using RefugioAnimal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace RefugioAnimal.Infrastructure.Persistence.SolicitudesAdopcion
{
    public class SolicitudAdopcionRepository : ISolicitudAdopcionRepository
    {
        private readonly RefugioContext _context;
        private readonly IMapper _mapper;

        public SolicitudAdopcionRepository(
            RefugioContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SolicitudAdopcionDto>> GetAll()
        {
            var solicitudes = await _context.SolicitudesAdopcion
                .Include(s => s.Adoptante)
                .Include(s => s.Mascota)
                .ToListAsync();

            return _mapper.Map<IEnumerable<SolicitudAdopcionDto>>(solicitudes);
        }

        public async Task<SolicitudAdopcionDto> GetById(Guid id)
        {
            var solicitud = await _context.SolicitudesAdopcion
                .Include(s => s.Adoptante)
                .Include(s => s.Mascota)
                .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<SolicitudAdopcionDto>(solicitud);
        }

        public async Task Insert(SolicitudAdopcionRequest solicitud)
        {
            var entity = _mapper.Map<Domain.Entities.SolicitudAdopcion>(solicitud);

            entity.Id = Guid.NewGuid();

            _context.SolicitudesAdopcion.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(SolicitudAdopcionRequest solicitud)
        {
            var entity = await _context.SolicitudesAdopcion
                .FirstOrDefaultAsync(s => s.Id == solicitud.Id);

            _mapper.Map(solicitud, entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.SolicitudesAdopcion
                .FirstOrDefaultAsync(s => s.Id == id);

            _context.SolicitudesAdopcion.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}