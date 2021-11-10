namespace MyApp.Core;

public interface ICharacterRepository
{
    Task<CharacterDetailsDto> CreateAsync(CharacterCreateDto character);
    Task<Option<CharacterDetailsDto>> ReadAsync(int characterId);
    Task<IReadOnlyCollection<CharacterDto>> ReadAsync();
    Task<Status> UpdateAsync(int id, CharacterUpdateDto character);
    Task<Status> DeleteAsync(int characterId);
}
