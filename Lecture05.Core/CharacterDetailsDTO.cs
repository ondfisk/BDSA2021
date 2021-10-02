using System;
using System.Collections.Generic;

namespace Lecture05.Core
{
    public record CharacterDetailsDTO : CharacterDTO
    {
        public string City { get; init; }
        public DateTime? FirstAppearance { get; init; }
        public IEnumerable<string> Powers { get; init; }
    }
}
