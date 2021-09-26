using System;
using System.Collections.Generic;

namespace Lecture04.Core
{
    public interface ICharacterRepository : IDisposable
    {
        int Create(CharacterDetailsDTO character);
        CharacterDetailsDTO Read(int characterId);
        IReadOnlyCollection<CharacterDTO> Read();
        void Update(CharacterDetailsDTO character);
        void Delete(int characterId);
    }
}
