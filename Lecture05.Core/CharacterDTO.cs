using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lecture05.Core
{
    public record CharacterDTO(int Id, string Name, string AlterEgo);

    public record CharacterDetailsDTO(int Id, string Name, string AlterEgo, string City, DateTime? FirstAppearance, IReadOnlyCollection<string> Powers) : CharacterDTO(Id, Name, AlterEgo);

    public record CharacterCreateDTO
    {
        [StringLength(50)]
        public string GivenName { get; init; }

        [StringLength(50)]
        public string Surname { get; init; }

        [Required]
        [StringLength(50)]
        public string AlterEgo { get; init; }

        public DateTime FirstAppearance { get; init; }

        [StringLength(50)]
        public string Occupation { get; init; }

        public string City { get; init; }

        public Gender Gender { get; init; }

        public IReadOnlyCollection<string> Powers { get; init; }
    }

    public record CharacterUpdateDTO : CharacterCreateDTO
    {
        public int Id { get; init; }
    }
}
