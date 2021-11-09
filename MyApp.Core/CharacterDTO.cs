using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Core
{
    public record CharacterDTO(int Id, string GivenName, string Surname, string AlterEgo);

    public record CharacterDetailsDTO(int Id, string GivenName, string Surname, string AlterEgo, string City, Gender gender, int? FirstAppearance, string Occupation, string ImageUrl, IReadOnlySet<string> Powers) : CharacterDTO(Id, GivenName, Surname, AlterEgo);

    public record CharacterCreateDTO
    {
        [StringLength(50)]
        public string GivenName { get; init; }

        [StringLength(50)]
        public string Surname { get; init; }

        [Required]
        [StringLength(50)]
        public string AlterEgo { get; init; }

        [Range(1900, 2100)]
        public int? FirstAppearance { get; init; }

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
