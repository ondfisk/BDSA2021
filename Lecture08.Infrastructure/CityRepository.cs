using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lecture08.Core;
using Microsoft.EntityFrameworkCore;
using static Lecture08.Core.Response;

namespace Lecture08.Infrastructure
{
    public class CityRepository : ICityRepository
    {
        private readonly IComicsContext _context;

        public CityRepository(IComicsContext context)
        {
            _context = context;
        }

        public async Task<(Response, CityDTO)> CreateAsync(CityCreateDTO city)
        {
            var conflict =
                await _context.Cities
                              .Where(c => c.Name == city.Name)
                              .Select(c => new CityDTO(c.Id, c.Name))
                              .FirstOrDefaultAsync();

            if (conflict != null)
            {
                return (Conflict, conflict);
            }

            var entity = new City { Name = city.Name };

            _context.Cities.Add(entity);

            await _context.SaveChangesAsync();

            return (Created, new CityDTO(entity.Id, entity.Name));
        }

        public async Task<CityDTO> ReadAsync(int cityId)
        {
            var cities = from c in _context.Cities
                         where c.Id == cityId
                         select new CityDTO(c.Id, c.Name);

            return await cities.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<CityDTO>> ReadAsync() =>
            (await _context.Cities
                           .Select(c => new CityDTO(c.Id, c.Name))
                           .ToListAsync())
                           .AsReadOnly();

        public async Task<Response> UpdateAsync(CityDTO city)
        {
            var conflict = await _context.Cities
                                   .Where(c => c.Id != city.Id)
                                   .Where(c => c.Name == city.Name)
                                   .Select(c => new CityDTO(c.Id, c.Name))
                                   .AnyAsync();

            if (conflict)
            {
                return Conflict;
            }

            var entity = await _context.Cities.FindAsync(city.Id);

            if (entity == null)
            {
                return NotFound;
            }

            entity.Name = city.Name;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<Response> DeleteAsync(int cityId)
        {
            var entity =
                await _context.Cities
                              .Include(c => c.Characters)
                              .FirstOrDefaultAsync(c => c.Id == cityId);

            if (entity == null)
            {
                return NotFound;
            }

            if (entity.Characters.Any())
            {
                return Conflict;
            }

            _context.Cities.Remove(entity);
            await _context.SaveChangesAsync();

            return Deleted;
        }
    }
}
