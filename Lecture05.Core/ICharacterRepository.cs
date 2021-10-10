using System;
using System.Collections.Generic;

namespace Lecture05.Core
{
    public interface ICharacterRepository
    {
        CharacterDetailsDTO Create(CharacterCreateDTO character);
        CharacterDetailsDTO Read(int characterId);
        IReadOnlyCollection<CharacterDTO> Read();
        Response Update(CharacterUpdateDTO character);
        Response Delete(int characterId);
    }
}
