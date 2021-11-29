namespace MyApp.Infrastructure.Tests;

public class CharacterRepositoryTests : IDisposable
{
    private readonly ComicsContext _context;
    private readonly CharacterRepository _repository;
    private bool disposedValue;

    public CharacterRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<ComicsContext>();
        builder.UseSqlite(connection);
        var context = new ComicsContext(builder.Options);
        context.Database.EnsureCreated();

        var metropolis = new City("Metropolis");
        var gothamCity = new City("Gotham City");
        var themyscira = new City("Themyscira");

        var superStrength = new Power("super strength");
        var flight = new Power("flight");
        var invulnerability = new Power("invulnerability");
        var superSpeed = new Power("super speed");
        var heatVision = new Power("heat vision");
        var freezeBreath = new Power("freeze breath");
        var xRayVision = new Power("x-ray vision");
        var superhumanHearing = new Power("superhuman hearing");
        var healingFactor = new Power("healing factor");
        var exceptionalMartialArtist = new Power("exceptional martial artist");
        var combatStrategy = new Power("combat strategy");
        var inexhaustibleWealth = new Power("inexhaustible wealth");
        var brilliantDeductiveSkill = new Power("brilliant deductive skill");
        var advancedTechnology = new Power("advanced technology");
        var combatSkill = new Power("combat skill");
        var superhumanAgility = new Power("superhuman weaponry");
        var magicWeaponry = new Power("magic agility");
        var gymnasticAbility = new Power("gymnastic ability");

        context.Characters.AddRange(
            new Character { Id = 1, GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = 1938, ImageUrl = "https://localhost/images/superman.png", Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
            new Character { Id = 2, GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = 1939, ImageUrl = "https://localhost/images/batman.png", Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
            new Character { Id = 3, GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = 1941, ImageUrl = "https://localhost/images/wonder-woman.png", Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
            new Character { Id = 4, GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = 1940, ImageUrl = "https://localhost/images/catwoman.png", Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
        );

        context.SaveChanges();

        _context = context;
        _repository = new CharacterRepository(_context);
    }

    [Fact]
    public async Task CreateAsync_creates_new_character_with_generated_id()
    {
        var character = new CharacterCreateDto
        {
            GivenName = "Harleen",
            Surname = "Quinzel",
            AlterEgo = "Harley Quinn",
            FirstAppearance = 1992,
            Occupation = "Former psychiatrist",
            City = "Gotham City",
            Gender = Female,
            ImageUrl = "https://localhost/images/harley-quinn.png",
            Powers = new HashSet<string> { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }
        };

        var created = await _repository.CreateAsync(character);

        Assert.Equal(5, created.Id);
        Assert.Equal("Harleen", created.GivenName);
        Assert.Equal("Quinzel", created.Surname);
        Assert.Equal("Harley Quinn", created.AlterEgo);
        Assert.Equal("Gotham City", created.City);
        Assert.Equal("Former psychiatrist", created.Occupation);
        Assert.Equal(1992, created.FirstAppearance);
        Assert.Equal(Female, created.Gender);
        Assert.Equal("https://localhost/images/harley-quinn.png", created.ImageUrl);
        Assert.True(created.Powers.SetEquals(new[] { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }));
    }

    [Fact]
    public async Task ReadAsync_returns_all_characters()
    {
        var characters = await _repository.ReadAsync();

        Assert.Collection(characters,
            character => Assert.Equal(new CharacterDto(1, "Superman", "Clark", "Kent"), character),
            character => Assert.Equal(new CharacterDto(2, "Batman", "Bruce", "Wayne"), character),
            character => Assert.Equal(new CharacterDto(3, "Wonder Woman", "Diana", "Prince"), character),
            character => Assert.Equal(new CharacterDto(4, "Catwoman", "Selina", "Kyle"), character)
        );
    }

    [Fact]
    public async Task ReadAsync_given_id_does_not_exist_returns_None()
    {
        var option = await _repository.ReadAsync(42);

        Assert.True(option.IsNone);
    }

    [Fact]
    public async Task ReadAsync_given_id_exists_returns_Character()
    {
        var option = await _repository.ReadAsync(4);

        var character = option.Value;

        Assert.Equal(4, character.Id);
        Assert.Equal("Selina", character.GivenName);
        Assert.Equal("Kyle", character.Surname);
        Assert.Equal("Catwoman", character.AlterEgo);
        Assert.Equal("Gotham City", character.City);
        Assert.Equal(1940, character.FirstAppearance);
        Assert.Equal("Thief", character.Occupation);
        Assert.Equal("https://localhost/images/catwoman.png", character.ImageUrl);
        Assert.True(character.Powers.SetEquals(new[] { "exceptional martial artist", "gymnastic ability", "combat skill" }));
    }

    [Fact]
    public async Task UpdateAsync_given_non_existing_id_returns_NotFound()
    {
        var character = new CharacterUpdateDto
        {
            Id = 42,
            AlterEgo = "Harley Quinn",
            Gender = Female,
            Powers = new HashSet<string>()
        };

        var updated = await _repository.UpdateAsync(42, character);

        Assert.Equal(NotFound, updated);
    }

    [Fact]
    public async Task UpdateAsync_updates_existing_character()
    {
        var character = new CharacterUpdateDto
        {
            Id = 1,
            GivenName = "Clark",
            Surname = "Kent",
            AlterEgo = "Superman",
            FirstAppearance = 1938,
            Occupation = "Reporter",
            City = "Metropolis",
            Gender = Male,
            ImageUrl = "https://localhost/images/superman.png",
            Powers = new HashSet<string>()
        };

        var updated = await _repository.UpdateAsync(1, character);

        Assert.Equal(Updated, updated);

        var option = await _repository.ReadAsync(1);
        var superman = option.Value;

        Assert.Empty(superman.Powers);
    }

    [Fact]
    public async Task DeleteAsync_given_non_existing_id_returns_NotFound()
    {
        var deleted = await _repository.DeleteAsync(42);

        Assert.Equal(NotFound, deleted);
    }

    [Fact]
    public async Task DeleteAsync_given_existing_id_deletes()
    {
        var deleted = await _repository.DeleteAsync(3);

        Assert.Equal(Deleted, deleted);
        Assert.Null(await _context.Characters.FindAsync(3));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~CharacterRepositoryTests()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
