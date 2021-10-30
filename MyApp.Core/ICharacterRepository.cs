using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core
{
    public interface ICharacterRepository
    {
        Task<CharacterDetailsDTO> CreateAsync(CharacterCreateDTO character);
        Task<CharacterDetailsDTO> ReadAsync(int characterId);
        Task<IReadOnlyCollection<CharacterDTO>> ReadAsync();
        Task<Status> UpdateAsync(CharacterUpdateDTO character);
        Task<Status> DeleteAsync(int characterId);
    }
}
