namespace Linq.Models;

public record Superhero
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AlterEgo { get; init; } = string.Empty;
    public string Occupation { get; init; } = string.Empty;
    public int? CityId { get; init; }
    public Gender Gender { get; init; }
    public int FirstAppearance { get; init; }
    public ICollection<string> Powers { get; init; } = new HashSet<string>();
    public ICollection<Group> GroupAffiliations { get; init; } = new HashSet<Group>();

    public override string ToString() => $"{Name} aka {AlterEgo} ({FirstAppearance})";
}

public record Superhero2
{
    public string GivenName { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string AlterEgo { get; init; } = string.Empty;
    public DateTime FirstAppearance { get; init; }
    public string City { get; init; } = string.Empty;

    public override string ToString() => $"{GivenName} {Surname} aka {AlterEgo}";
}
