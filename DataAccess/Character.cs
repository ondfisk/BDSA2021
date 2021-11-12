namespace DataAccess;

public class Character
{
    public int Id { get; set; }

    public int? ActorId { get; set; }

    public Actor? Actor { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(50)]
    public string? Species { get; set; }

    [StringLength(50)]
    public string? Planet { get; set; }

    public Character(string name)
    {
        Name = name;
    }

    public override string ToString() => $"Character {{ Id = {Id}, ActorId = {ActorId}, Actor = {Actor?.Name}, Name = {Name}, Species = {Species}, Planet = {Planet} }}";
}
