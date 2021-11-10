namespace Linq.Models;

public record Superhero
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? AlterEgo { get; init; }
    public string? Occupation { get; init; }
    public int? CityId { get; init; }
    public Gender Gender { get; init; }
    public int FirstAppearance { get; init; }
    public ICollection<string>? Powers { get; init; }
    public ICollection<Group>? GroupAffiliations { get; init; }

    public override string ToString() => $"{Name} aka {AlterEgo} ({FirstAppearance})";
}

public record Superhero2
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string AlterEgo { get; init; }
    public DateTime FirstAppearance { get; init; }
    public string City { get; init; }

    public override string ToString() => $"{GivenName} {Surname} aka {AlterEgo}";
}
