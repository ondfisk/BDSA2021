using System;
using System.Collections.Generic;
using System.Linq;
using Lecture05.Core;

namespace Lecture05.Entities
{
    public class CityRepository : ICityRepository
    {
        private readonly IComicsContext _context;

        public CityRepository(IComicsContext context)
        {
            _context = context;
        }

        public CityDTO Create(CityCreateDTO city)
        {
            var entity = new City { Name = city.Name };

            _context.Cities.Add(entity);

            _context.SaveChanges();

            return new CityDTO(entity.Id, entity.Name);
        }

        public Response Delete(int cityId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        public CityDTO Read(int cityId)
        {
            var cities = from c in _context.Cities
                         where c.Id == cityId
                         select new CityDTO(c.Id, c.Name);

            return cities.FirstOrDefault();
        }

        public IReadOnlyCollection<CityDTO> Read()
        {
            throw new NotImplementedException();
        }

        public Response Update(CityDTO city)
        {
            throw new NotImplementedException();
        }
    }
}
