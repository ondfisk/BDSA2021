using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core;
using Microsoft.EntityFrameworkCore;
using static MyApp.Core.Status;

namespace MyApp.Infrastructure
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IComicsContext _context;

        public CharacterRepository(IComicsContext context)
        {
            _context = context;
        }

        public async Task<CharacterDetailsDTO> CreateAsync(CharacterCreateDTO character)
        {
            var entity = new Character
            {
                GivenName = character.GivenName,
                Surname = character.Surname,
                AlterEgo = character.AlterEgo,
                FirstAppearance = character.FirstAppearance,
                Occupation = character.Occupation,
                City = await GetCityAsync(character.City),
                Gender = character.Gender,
                Powers = await GetPowersAsync(character.Powers).ToListAsync()
            };

            _context.Characters.Add(entity);

            await _context.SaveChangesAsync();

            return await ReadAsync(entity.Id);
        }

        public async Task<CharacterDetailsDTO> ReadAsync(int characterId)
        {
            var characters = from c in _context.Characters
                             where c.Id == characterId
                             select new CharacterDetailsDTO(
                                 c.Id,
                                 c.GivenName,
                                 c.Surname,
                                 c.AlterEgo,
                                 c.City.Name,
                                 c.Gender,
                                 c.FirstAppearance,
                                 c.Occupation,
                                 c.Powers.Select(c => c.Name).ToHashSet()
                             );

            return await characters.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<CharacterDTO>> ReadAsync() =>
            (await _context.Characters
                           .Select(c => new CharacterDTO(c.Id, c.GivenName, c.Surname, c.AlterEgo))
                           .ToListAsync())
                           .AsReadOnly();

        public async Task<Status> UpdateAsync(CharacterUpdateDTO character)
        {
            var entity = await _context.Characters.Include(c => c.Powers).FirstOrDefaultAsync(c => c.Id == character.Id);

            if (entity == null)
            {
                return NotFound;
            }

            entity.GivenName = character.GivenName;
            entity.Surname = character.Surname;
            entity.AlterEgo = character.AlterEgo;
            entity.FirstAppearance = character.FirstAppearance;
            entity.Occupation = character.Occupation;
            entity.City = await GetCityAsync(character.City);
            entity.Gender = character.Gender;
            entity.Powers = await GetPowersAsync(character.Powers).ToListAsync();

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<Status> DeleteAsync(int characterId)
        {
            var entity = await _context.Characters.FindAsync(characterId);

            if (entity == null)
            {
                return NotFound;
            }

            _context.Characters.Remove(entity);
            await _context.SaveChangesAsync();

            return Deleted;
        }

        private async Task<City> GetCityAsync(string name) =>
            await _context.Cities.FirstOrDefaultAsync(c => c.Name == name) ??
            new City { Name = name };

        private async IAsyncEnumerable<Power> GetPowersAsync(IEnumerable<string> powers)
        {
            var existing = await _context.Powers.Where(p => powers.Contains(p.Name)).ToDictionaryAsync(p => p.Name);

            foreach (var power in powers)
            {
                yield return existing.TryGetValue(power, out var p) ? p : new Power { Name = power };
            }
        }
    }
}
