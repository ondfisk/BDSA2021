using System.Text.Json.Serialization;

namespace MyApp.Core;

public record CharacterDto(int Id, string? AlterEgo, string? GivenName, string? Surname);

public record CharacterDetailsDto(int Id, string? AlterEgo, string? GivenName, string? Surname, string? City, Gender Gender, int? FirstAppearance, string? Occupation, string? ImageUrl, ISet<string> Powers) : CharacterDto(Id, AlterEgo, GivenName, Surname);

public record CharacterCreateDto
{
    [CustomValidation(typeof(CharacterValidation), nameof(CharacterValidation.ValidateName))]
    [StringLength(50)]
    public string? AlterEgo { get; set; }

    [StringLength(50)]
    public string? GivenName { get; set; }

    [StringLength(50)]
    public string? Surname { get; set; }

    [Range(1900, 2100)]
    public int? FirstAppearance { get; set; }

    [StringLength(50)]
    public string? Occupation { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    public Gender Gender { get; set; }

    [StringLength(250)]
    [Url]
    public string? ImageUrl { get; set; }

    [CustomValidation(typeof(CharacterValidation), nameof(CharacterValidation.ValidatePowers))]
    [Required]
    public ISet<string> Powers { get; set; } = new HashSet<string>();
}

public record CharacterUpdateDto : CharacterCreateDto
{
    public int Id { get; set; }
}
