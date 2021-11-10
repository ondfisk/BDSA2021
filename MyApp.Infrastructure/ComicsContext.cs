namespace MyApp.Infrastructure;

public class ComicsContext : DbContext, IComicsContext
{
    public DbSet<City> Cities => Set<City>();
    public DbSet<Power> Powers => Set<Power>();
    public DbSet<Character> Characters => Set<Character>();

    public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Character>()
            .Property(e => e.Gender)
            .HasMaxLength(50)
            .HasConversion(new EnumToStringConverter<Gender>());

        modelBuilder.Entity<City>()
                    .HasIndex(s => s.Name)
                    .IsUnique();

        modelBuilder.Entity<Power>()
                    .HasIndex(p => p.Name)
                    .IsUnique();
    }
}
