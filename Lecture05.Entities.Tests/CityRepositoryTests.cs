using System;
using Lecture05.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lecture05.Entities.Tests
{
    public class CityRepositoryTests : IDisposable
    {
        private readonly IComicsContext _context;
        private readonly CityRepository _repo;

        public CityRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ComicsContext>();
            builder.UseSqlite(connection);
            var context = new ComicsContext(builder.Options);
            context.Database.EnsureCreated();
            context.Cities.Add(new City { Name = "Metropolis" });
            context.SaveChanges();

            _context = context;
            _repo = new CityRepository(_context);
        }

        [Fact]
        public void Create_given_City_returns_city_with_Id()
        {
            var city = new CityCreateDTO("Gotham City");

            var created = _repo.Create(city);

            Assert.Equal(new CityDTO(2, "Gotham City"), created);
        }

        [Fact]
        public void Read_given_non_existing_id_returns_null()
        {
            var city = _repo.Read(42);

            Assert.Null(city);
        }

        [Fact]
        public void Read_given_existing_id_returns_city()
        {
            var city = _repo.Read(1);

            Assert.Equal(new CityDTO(1, "Metropolis"), city);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
