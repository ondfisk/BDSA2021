using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lecture07.Core
{
    public record CharacterDTO(int Id, string GivenName, string Surname, string AlterEgo);

    public record CharacterDetailsDTO(int Id, string GivenName, string Surname, string AlterEgo, string City, DateTime? FirstAppearance, string Occupation, IReadOnlySet<string> Powers) : CharacterDTO(Id, GivenName, Surname, AlterEgo);

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

        [Required]
        public ISet<string> Powers { get; init; }
    }

    public record CharacterUpdateDTO : CharacterCreateDTO
    {
        public int Id { get; init; }
    }
}
