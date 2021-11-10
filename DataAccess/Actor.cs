namespace DataAccess;

public class Actor
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Character> Characters { get; set; } = null!;
}
