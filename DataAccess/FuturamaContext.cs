namespace DataAccess;

public class FuturamaContext : DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Actor> Actors => Set<Actor>();
    public FuturamaContext(DbContextOptions<FuturamaContext> options) : base(options) { }
}
