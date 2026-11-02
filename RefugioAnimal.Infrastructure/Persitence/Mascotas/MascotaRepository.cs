using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RefugioAnimal.Application.Features.Mascota;
using RefugioAnimal.Application.Features.Mascota.Contratos;
using RefugioAnimal.Infrastructure.Data;

namespace RefugioAnimal.Infrastructure.Persistence.Mascotas
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly RefugioContext _context;
        private readonly IMapper _mapper;

        public MascotaRepository(RefugioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MascotaDto>> GetAll()
        {
            var mascotas = await _context.Mascotas
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MascotaDto>>(mascotas);
        }

        public async Task<MascotaDto> GetById(Guid id)
        {
            var mascota = await _context.Mascotas
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<MascotaDto>(mascota);
        }

        public async Task Insert(MascotaRequest mascota)
        {
            var entity = _mapper.Map<Domain.Entities.Mascota>(mascota);

            entity.Id = Guid.NewGuid();

            _context.Mascotas.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(MascotaRequest mascota)
        {
            var entity = await _context.Mascotas
                .FirstOrDefaultAsync(x => x.Id == mascota.Id);

            if (entity == null)
                return;

            _mapper.Map(mascota, entity);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await _context.Mascotas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return;

            _context.Mascotas.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}