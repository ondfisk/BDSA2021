using System.Collections.Generic;
using System.Linq;
using Lecture05.Core;
using Microsoft.EntityFrameworkCore;
using static Lecture05.Core.Response;

namespace Lecture05.Infrastructure
{
    public class CityRepository : ICityRepository
    {
        private readonly IComicsContext _context;

        public CityRepository(IComicsContext context)
        {
            _context = context;
        }

        public (Response, CityDTO) Create(CityCreateDTO city)
        {
            var conflict = _context.Cities
                                   .Where(c => c.Name == city.Name)
                                   .Select(c => new CityDTO(c.Id, c.Name))
                                   .FirstOrDefault();

            if (conflict != null)
            {
                return (Conflict, conflict);
            }

            var entity = new City { Name = city.Name };

            _context.Cities.Add(entity);

            _context.SaveChanges();

            return (Created, new CityDTO(entity.Id, entity.Name));
        }

        public CityDTO Read(int cityId)
        {
            var cities = from c in _context.Cities
                         where c.Id == cityId
                         select new CityDTO(c.Id, c.Name);

            return cities.FirstOrDefault();
        }

        public IReadOnlyCollection<CityDTO> Read() =>
            _context.Cities
                    .Select(c => new CityDTO(c.Id, c.Name))
                    .ToList()
                    .AsReadOnly();

        public Response Update(CityDTO city)
        {
            var conflict = _context.Cities
                                   .Where(c => c.Id != city.Id)
                                   .Where(c => c.Name == city.Name)
                                   .Select(c => new CityDTO(c.Id, c.Name))
                                   .Any();

            if (conflict)
            {
                return Conflict;
            }

            var entity = _context.Cities.Find(city.Id);

            if (entity == null)
            {
                return NotFound;
            }

            entity.Name = city.Name;

            _context.SaveChanges();

            return Updated;
        }

        public Response Delete(int cityId)
        {
            var entity = _context.Cities.Include(c => c.Characters).FirstOrDefault(c => c.Id == cityId);

            if (entity == null)
            {
                return NotFound;
            }

            if (entity.Characters.Any())
            {
                return Conflict;
            }

            _context.Cities.Remove(entity);
            _context.SaveChanges();

            return Deleted;
        }
    }
}
