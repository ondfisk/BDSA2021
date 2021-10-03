using System;
using System.Collections.Generic;
using Lecture05.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using static Lecture05.Core.Gender;
using static Lecture05.Core.Response;

namespace Lecture05.Entities.Tests
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
        public void Create_creates_new_character_with_generated_id()
        {
            var repository = new CharacterRepository(_context);

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

            var created = repository.Create(character);

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
        public void Read_returns_all_characters()
        {
            var characters = _repository.Read();

            Assert.Collection(characters,
                c => Assert.Equal(new CharacterDTO(1, "Clark", "Kent", "Superman"), c),
                c => Assert.Equal(new CharacterDTO(2, "Bruce", "Wayne", "Batman"), c),
                c => Assert.Equal(new CharacterDTO(3, "Diana", "Prince", "Wonder Woman"), c),
                c => Assert.Equal(new CharacterDTO(4, "Selina", "Kyle", "Catwoman"), c)
            );
        }

        [Fact]
        public void Read_given_id_does_not_exist_returns_null()
        {
            var character = _repository.Read(42);

            Assert.Null(character);
        }

        [Fact]
        public void Read_given_id_exists_returns_Character()
        {
            var character = _repository.Read(4);

            Assert.Equal(4, character.Id);
            Assert.Equal("Selina", character.GivenName);
            Assert.Equal("Kyle", character.Surname);
            Assert.Equal("Catwoman", character.AlterEgo);
            Assert.Equal("Gotham City", character.City);
            Assert.Equal(DateTime.Parse("1940-04-01"), character.FirstAppearance);
            Assert.True(character.Powers.SetEquals(new[] { "exceptional martial artist", "gymnastic ability", "combat skill" }));
        }

        [Fact]
        public void Update_given_non_existing_id_returns_NotFound()
        {
            var repository = new CharacterRepository(_context);

            var character = new CharacterUpdateDTO
            {
                Id = 42,
                AlterEgo = "Harley Quinn",
                Gender = Female,
                Powers = new HashSet<string>()
            };

            var updated = repository.Update(character);

            Assert.Equal(NotFound, updated);
        }

        [Fact]
        public void Update_updates_existing_character()
        {
            var repository = new CharacterRepository(_context);

            var character = new CharacterUpdateDTO
            {
                Id = 3,
                GivenName = "Barry",
                Surname = "Allen",
                AlterEgo = "The Flash",
                FirstAppearance = DateTime.Parse("1956-10-01"),
                Occupation = "Forensic scientist",
                City = "Central City",
                Gender = Male,
                Powers = new HashSet<string> { "super speed", "intangibility", "superhuman agility", "time travel", "creates and controls lightning", "multiversal knowledge" }
            };

            var updated = repository.Update(character);

            Assert.Equal(Updated, updated);

            var flash = repository.Read(3);
            Assert.Equal(3, flash.Id);
            Assert.Equal("Barry", flash.GivenName);
            Assert.Equal("Allen", flash.Surname);
            Assert.Equal("The Flash", flash.AlterEgo);
            Assert.Equal(DateTime.Parse("1956-10-01"), flash.FirstAppearance);
            Assert.Equal("Forensic scientist", flash.Occupation);
            Assert.Equal("Central City", flash.City);
            Assert.True(flash.Powers.SetEquals(new[] { "super speed", "intangibility", "superhuman agility", "time travel", "creates and controls lightning", "multiversal knowledge" }));
        }

        [Fact]
        public void Delete_given_non_existing_id_returns_NotFound()
        {
            var repository = new CharacterRepository(_context);

            var deleted = repository.Delete(42);

            Assert.Equal(NotFound, deleted);
        }

        [Fact]
        public void Delete_given_existing_id_deletes()
        {
            var repository = new CharacterRepository(_context);

            var deleted = repository.Delete(3);

            Assert.Equal(Deleted, deleted);
            Assert.Null(_context.Characters.Find(3));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
