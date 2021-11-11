namespace DataAccess;

public class Actor
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<Character> Characters { get; set; } = null!;

    public Actor(string name)
    {
        Name = name;
    }

    public override string ToString() => $"Actor {{ Id = {Id}, Name = {Name} }}";
}
