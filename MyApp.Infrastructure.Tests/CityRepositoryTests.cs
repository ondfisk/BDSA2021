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
            context.Cities.AddRange(new City("Metropolis") { Id = 1 }, new City("Gotham City") { Id = 2 });
            context.Characters.Add(new Character { Id = 1, AlterEgo = "Superman", CityId = 1 });
            context.SaveChanges();

            _context = context;
            _repository = new CityRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_given_City_returns_Created_with_City()
        {
            var city = new CityCreateDto("Central City");

            var created = await _repository.CreateAsync(city);

            Assert.Equal((Created, new CityDto(3, "Central City")), created);
        }

        [Fact]
        public async Task CreateAsync_given_existing_City_returns_Conflict_with_existing_City()
        {
            var city = new CityCreateDto("Gotham City");

            var created = await _repository.CreateAsync(city);

            Assert.Equal((Conflict, new CityDto(2, "Gotham City")), created);
        }

        [Fact]
        public async Task ReadAsync_given_non_existing_id_returns_None()
        {
            var option = await _repository.ReadAsync(42);

            Assert.True(option.IsNone);
        }

        [Fact]
        public async Task ReadAsync_given_existing_id_returns_city()
        {
            var option = await _repository.ReadAsync(2);

            Assert.Equal(new CityDto(2, "Gotham City"), option.Value);
        }

        [Fact]
        public async Task ReadAsync_returns_all_cities()
        {
            var cities = await _repository.ReadAsync();

            Assert.Collection(cities,
                city => Assert.Equal(new CityDto(1, "Metropolis"), city),
                city => Assert.Equal(new CityDto(2, "Gotham City"), city)
            );
        }

        [Fact]
        public async Task UpdateAsync_given_non_existing_City_returns_NotFound()
        {
            var city = new CityDto(42, "Central City");

            var response = await _repository.UpdateAsync(city);

            Assert.Equal(NotFound, response);
        }

        [Fact]
        public async Task UpdateAsync_given_existing_name_returns_Conflict()
        {
            var city = new CityDto(2, "Metropolis");

            var response = await _repository.UpdateAsync(city);

            Assert.Equal(Conflict, response);
        }

        [Fact]
        public async Task UpdateAsync_updates_and_returns_Updated()
        {
            var city = new CityDto(2, "Central City");

            var response = await _repository.UpdateAsync(city);

            var entity = await _context.Cities.FirstAsync(c => c.Name == "Central City");

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

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
