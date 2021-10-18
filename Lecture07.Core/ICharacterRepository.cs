using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecture07.Core
{
    public interface ICharacterRepository
    {
        Task<CharacterDetailsDTO> CreateAsync(CharacterCreateDTO character);
        Task<CharacterDetailsDTO> ReadAsync(int characterId);
        Task<IReadOnlyCollection<CharacterDTO>> ReadAsync();
        Task<Response> UpdateAsync(CharacterUpdateDTO character);
        Task<Response> DeleteAsync(int characterId);
    }
}
