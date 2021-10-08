using System;
using System.Collections.Generic;
using System.Linq;
using Lecture05.Core;
using static Lecture05.Core.Response;

namespace Lecture05.Entities
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IComicsContext _context;

        public CharacterRepository(IComicsContext context)
        {
            _context = context;
        }

        public CharacterDetailsDTO Create(CharacterCreateDTO character)
        {
            var entity = new Character
            {
                GivenName = character.GivenName,
                Surname = character.Surname,
                AlterEgo = character.AlterEgo,
                FirstAppearance = character.FirstAppearance,
                Occupation = character.Occupation,
                City = GetCity(character.City),
                Gender = character.Gender,
                Powers = GetPowers(character.Powers).ToList()
            };

            _context.Characters.Add(entity);

            _context.SaveChanges();

            return Read(entity.Id);
        }

        public CharacterDetailsDTO Read(int characterId)
        {
            var characters = from c in _context.Characters
                             where c.Id == characterId
                             select new CharacterDetailsDTO(
                                 c.Id,
                                 c.GivenName,
                                 c.Surname,
                                 c.AlterEgo,
                                 c.City.Name,
                                 c.FirstAppearance,
                                 c.Occupation,
                                 c.Powers.Select(c => c.Name).ToHashSet()
                             );

            return characters.FirstOrDefault();
        }

        public IReadOnlyCollection<CharacterDTO> Read() =>
            _context.Characters
                    .Select(c => new CharacterDTO(c.Id, c.GivenName, c.Surname, c.AlterEgo))
                    .ToList().AsReadOnly();

        public Response Update(CharacterUpdateDTO character)
        {
            var entity = _context.Characters.Find(character.Id);

            if (entity == null)
            {
                return NotFound;
            }

            entity.GivenName = character.GivenName;
            entity.Surname = character.Surname;
            entity.AlterEgo = character.AlterEgo;
            entity.FirstAppearance = character.FirstAppearance;
            entity.Occupation = character.Occupation;
            entity.City = GetCity(character.City);
            entity.Gender = character.Gender;
            entity.Powers = GetPowers(character.Powers).ToList();

            _context.SaveChanges();

            return Updated;
        }

        public Response Delete(int characterId)
        {
            var entity = _context.Characters.Find(characterId);

            if (entity == null)
            {
                return NotFound;
            }

            _context.Characters.Remove(entity);
            _context.SaveChanges();

            return Deleted;
        }

        private City GetCity(string name) =>
            _context.Cities.FirstOrDefault(c => c.Name == name) ??
            new City { Name = name };

        private IEnumerable<Power> GetPowers(IEnumerable<string> powers)
        {
            var existing = _context.Powers.Where(p => powers.Contains(p.Name)).ToDictionary(p => p.Name);

            foreach (var power in powers)
            {
                yield return existing.TryGetValue(power, out var p) ? p : new Power { Name = power };
            }
        }
    }
}
