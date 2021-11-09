using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core
{
    public interface ICharacterRepository
    {
        Task<CharacterDetailsDto> CreateAsync(CharacterCreateDto character);
        Task<CharacterDetailsDto> ReadAsync(int characterId);
        Task<IReadOnlyCollection<CharacterDto>> ReadAsync();
        Task<Status> UpdateAsync(CharacterUpdateDto character);
        Task<Status> DeleteAsync(int characterId);
    }
}
