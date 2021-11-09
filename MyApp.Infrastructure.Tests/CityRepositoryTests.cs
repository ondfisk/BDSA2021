using System;
using System.Threading.Tasks;
using MyApp.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static MyApp.Core.Status;

namespace MyApp.Infrastructure.Tests
{
    public class CityRepositoryTests : IDisposable
    {
        private readonly IComicsContext _context;
        private readonly CityRepository _repository;

        public CityRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ComicsContext>();
            builder.UseSqlite(connection);
            var context = new ComicsContext(builder.Options);
            context.Database.EnsureCreated();
            context.Cities.Add(new City { Id = 1, Name = "Metropolis" });
            context.Cities.Add(new City { Id = 2, Name = "Gotham City" });
            context.Characters.Add(new Character { Id = 1, AlterEgo = "Superman", CityId = 1 });
            context.SaveChanges();

            _context = context;
            _repository = new CityRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_given_City_returns_Created_with_City()
        {
            var city = new CityCreateDTO("Central City");

            var created = await _repository.CreateAsync(city);

            Assert.Equal((Created, new CityDTO(3, "Central City")), created);
        }

        [Fact]
        public async Task CreateAsync_given_existing_City_returns_Conflict_with_existing_City()
        {
            var city = new CityCreateDTO("Gotham City");

            var created = await _repository.CreateAsync(city);

            Assert.Equal((Conflict, new CityDTO(2, "Gotham City")), created);
        }

        [Fact]
        public async Task ReadAsync_given_non_existing_id_returns_null()
        {
            var city = await _repository.ReadAsync(42);

            Assert.Null(city);
        }

        [Fact]
        public async Task ReadAsync_given_existing_id_returns_city()
        {
            var city = await _repository.ReadAsync(2);

            Assert.Equal(new CityDTO(2, "Gotham City"), city);
        }

        [Fact]
        public async Task ReadAsync_returns_all_cities()
        {
            var cities = await _repository.ReadAsync();

            Assert.Collection(cities,
                city => Assert.Equal(new CityDTO(1, "Metropolis"), city),
                city => Assert.Equal(new CityDTO(2, "Gotham City"), city)
            );
        }

        [Fact]
        public async Task UpdateAsync_given_non_existing_City_returns_NotFound()
        {
            var city = new CityDTO(42, "Central City");

            var response = await _repository.UpdateAsync(city);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public async Task UpdateAsync_given_existing_name_returns_Conflict()
        {
            var city = new CityDTO(2, "Metropolis");

            var response = await _repository.UpdateAsync(city);

            Assert.Equal(Conflict, response);
        }

        [Fact]
        public async Task UpdateAsync_updates_and_returns_Updated()
        {
            var city = new CityDTO(2, "Central City");

            var response = await _repository.UpdateAsync(city);

            var entity = await _context.Cities.FirstOrDefaultAsync(c => c.Name == "Central City");

            Assert.Equal(Updated, response);


            Assert.Equal(2, entity.Id);
        }

        [Fact]
        public async Task DeleteAsync_given_non_existing_Id_returns_NotFound()
        {
            var response = await _repository.DeleteAsync(42);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public async Task DeleteAsync_deletes_and_returns_Deleted()
        {
            var response = await _repository.DeleteAsync(2);

            var entity = await _context.Cities.FindAsync(2);

            Assert.Equal(Deleted, response);
            Assert.Null(entity);
        }

        [Fact]
        public async Task Delete_given_existing_City_with_Characters_does_not_delete_and_returns_Conflict()
        {
            var response = await _repository.DeleteAsync(1);
            var entity = await _context.Cities.FindAsync(1);

            Assert.Equal(Conflict, response);
            Assert.NotNull(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
