using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lecture08.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Lecture08.Core.Gender;
using static Lecture08.Core.Response;

namespace Lecture08.Infrastructure.Tests
{
    public class CharacterRepositoryTests : IDisposable
    {
        private readonly ComicsContext _context;
        private readonly CharacterRepository _repository;

        public CharacterRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<ComicsContext>();
            builder.UseSqlite(connection);
            var context = new ComicsContext(builder.Options);
            context.Database.EnsureCreated();

            var metropolis = new City { Id = 1, Name = "Metropolis" };
            var gothamCity = new City { Id = 2, Name = "Gotham City" };
            var themyscira = new City { Id = 3, Name = "Themyscira" };

            var superStrength = new Power { Name = "super strength" };
            var flight = new Power { Name = "flight" };
            var invulnerability = new Power { Name = "invulnerability" };
            var superSpeed = new Power { Name = "super speed" };
            var heatVision = new Power { Name = "heat vision" };
            var freezeBreath = new Power { Name = "freeze breath" };
            var xRayVision = new Power { Name = "x-ray vision" };
            var superhumanHearing = new Power { Name = "superhuman hearing" };
            var healingFactor = new Power { Name = "healing factor" };
            var exceptionalMartialArtist = new Power { Name = "exceptional martial artist" };
            var combatStrategy = new Power { Name = "combat strategy" };
            var inexhaustibleWealth = new Power { Name = "inexhaustible wealth" };
            var brilliantDeductiveSkill = new Power { Name = "brilliant deductive skill" };
            var advancedTechnology = new Power { Name = "advanced technology" };
            var combatSkill = new Power { Name = "combat skill" };
            var superhumanAgility = new Power { Name = "superhuman weaponry" };
            var magicWeaponry = new Power { Name = "magic agility" };
            var gymnasticAbility = new Power { Name = "gymnastic ability" };

            context.Characters.AddRange(
                new Character { Id = 1, GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = DateTime.Parse("1938-04-18"), Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
                new Character { Id = 2, GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = DateTime.Parse("1939-05-01"), Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
                new Character { Id = 3, GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = DateTime.Parse("1941-10-21"), Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
                new Character { Id = 4, GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = DateTime.Parse("1940-04-01"), Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
            );

            context.SaveChanges();

            _context = context;
            _repository = new CharacterRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_creates_new_character_with_generated_id()
        {
            var character = new CharacterCreateDTO
            {
                GivenName = "Harleen",
                Surname = "Quinzel",
                AlterEgo = "Harley Quinn",
                FirstAppearance = DateTime.Parse("1992-09-11"),
                Occupation = "Former psychiatrist",
                City = "Gotham City",
                Gender = Female,
                Powers = new HashSet<string> { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }
            };

            var created = await _repository.CreateAsync(character);

            Assert.Equal(5, created.Id);
            Assert.Equal("Harleen", created.GivenName);
            Assert.Equal("Quinzel", created.Surname);
            Assert.Equal("Harley Quinn", created.AlterEgo);
            Assert.Equal("Gotham City", created.City);
            Assert.Equal("Former psychiatrist", created.Occupation);
            Assert.Equal(DateTime.Parse("1992-09-11"), created.FirstAppearance);
            Assert.True(created.Powers.SetEquals(new[] { "complete unpredictability", "superhuman agility", "skilled fighter", "intelligence", "emotional manipulation", "immunity to toxins" }));
        }

        [Fact]
        public async Task ReadAsync_returns_all_characters()
        {
            var characters = await _repository.ReadAsync();

            Assert.Collection(characters,
                character => Assert.Equal(new CharacterDTO(1, "Clark", "Kent", "Superman"), character),
                character => Assert.Equal(new CharacterDTO(2, "Bruce", "Wayne", "Batman"), character),
                character => Assert.Equal(new CharacterDTO(3, "Diana", "Prince", "Wonder Woman"), character),
                character => Assert.Equal(new CharacterDTO(4, "Selina", "Kyle", "Catwoman"), character)
            );
        }

        [Fact]
        public async Task ReadAsync_given_id_does_not_exist_returns_null()
        {
            var character = await _repository.ReadAsync(42);

            Assert.Null(character);
        }

        [Fact]
        public async Task ReadAsync_given_id_exists_returns_Character()
        {
            var character = await _repository.ReadAsync(4);

            Assert.Equal(4, character.Id);
            Assert.Equal("Selina", character.GivenName);
            Assert.Equal("Kyle", character.Surname);
            Assert.Equal("Catwoman", character.AlterEgo);
            Assert.Equal("Gotham City", character.City);
            Assert.Equal(DateTime.Parse("1940-04-01"), character.FirstAppearance);
            Assert.Equal("Thief", character.Occupation);
            Assert.True(character.Powers.SetEquals(new[] { "exceptional martial artist", "gymnastic ability", "combat skill" }));
        }

        [Fact]
        public async Task UpdateAsync_given_non_existing_id_returns_NotFound()
        {
            var character = new CharacterUpdateDTO
            {
                Id = 42,
                AlterEgo = "Harley Quinn",
                Gender = Female,
                Powers = new HashSet<string>()
            };

            var updated = await _repository.UpdateAsync(character);

            Assert.Equal(NotFound, updated);
        }

        [Fact]
        public async Task UpdateAsync_updates_existing_character()
        {
            var character = new CharacterUpdateDTO
            {
                Id = 1,
                GivenName = "Clark",
                Surname = "Kent",
                AlterEgo = "Superman",
                FirstAppearance = DateTime.Parse("1938-04-18"),
                Occupation = "Reporter",
                City = "Metropolis",
                Gender = Male,
                Powers = new HashSet<string>()
            };

            var updated = await _repository.UpdateAsync(character);

            Assert.Equal(Updated, updated);

            var superman = await _repository.ReadAsync(1);
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
