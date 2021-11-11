namespace MyApp.Infrastructure;

public class City
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<Character> Characters { get; set; } = null!;

    public City(string name)
    {
        Name = name;
    }
}
