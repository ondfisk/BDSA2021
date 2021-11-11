namespace MyApp.Infrastructure;

public class Power
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<Character> Characters { get; set; } = null!;

    public Power(string name)
    {
        Name = name;
    }
}
