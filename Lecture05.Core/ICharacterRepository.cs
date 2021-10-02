using System;
using System.Collections.Generic;

namespace Lecture05.Core
{
    public interface ICharacterRepository : IDisposable
    {
        CharacterDetailsDTO Create(CharacterCreateDTO character);
        CharacterDetailsDTO Read(int characterId);
        IReadOnlyCollection<CharacterDTO> Read();
        void Update(CharacterUpdateDTO character);
        void Delete(int characterId);
    }
}
