namespace MyApp.Infrastructure;

public class CharacterRepository : ICharacterRepository
{
    private readonly IComicsContext _context;

    public CharacterRepository(IComicsContext context)
    {
        _context = context;
    }

    public async Task<CharacterDetailsDto> CreateAsync(CharacterCreateDto character)
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

        return new CharacterDetailsDto(
                             entity.Id,
                             entity.GivenName,
                             entity.Surname,
                             entity.AlterEgo,
                             entity.City?.Name,
                             entity.Gender,
                             entity.FirstAppearance,
                             entity.Occupation,
                             entity.ImageUrl,
                             entity.Powers.Select(c => c.Name).ToHashSet()
                         );
    }

    public async Task<Option<CharacterDetailsDto>> ReadAsync(int characterId)
    {
        var characters = from c in _context.Characters
                         where c.Id == characterId
                         select new CharacterDetailsDto(
                             c.Id,
                             c.GivenName,
                             c.Surname,
                             c.AlterEgo,
                             c.City == null ? null : c.City.Name,
                             c.Gender,
                             c.FirstAppearance,
                             c.Occupation,
                             c.ImageUrl,
                             c.Powers.Select(c => c.Name).ToHashSet()
                         );

        return await characters.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<CharacterDto>> ReadAsync() =>
        (await _context.Characters
                       .Select(c => new CharacterDto(c.Id, c.GivenName, c.Surname, c.AlterEgo))
                       .ToListAsync())
                       .AsReadOnly();

    public async Task<Status> UpdateAsync(int id, CharacterUpdateDto character)
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

    private async Task<City?> GetCityAsync(string? name) =>
        string.IsNullOrWhiteSpace(name) ? null : await _context.Cities.FirstOrDefaultAsync(c => c.Name == name) ?? new City(name);

    private async IAsyncEnumerable<Power> GetPowersAsync(IEnumerable<string> powers)
    {
        var existing = await _context.Powers.Where(p => powers.Contains(p.Name)).ToDictionaryAsync(p => p.Name);

        foreach (var power in powers)
        {
            yield return existing.TryGetValue(power, out var p) ? p : new Power(power);
        }
    }
}
