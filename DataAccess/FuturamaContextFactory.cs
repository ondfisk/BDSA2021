namespace DataAccess;

public class FuturamaContextFactory : IDesignTimeDbContextFactory<FuturamaContext>
{
    public FuturamaContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Futurama");

        var optionsBuilder = new DbContextOptionsBuilder<FuturamaContext>()
            .UseSqlServer(connectionString);

        return new FuturamaContext(optionsBuilder.Options);
    }

    public static void Seed(FuturamaContext context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.Characters");
        context.Database.ExecuteSqlRaw("DELETE dbo.Actors");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Characters', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Actors', RESEED, 0)");

        var billyWest = new Actor("Billy West");
        var kateySagal = new Actor("Katey Sagal");
        var johnDiMaggio = new Actor("John DiMaggio");
        var laurenTom = new Actor("Lauren Tom");
        var philLaMarr = new Actor("Phil LaMarr");

        context.Characters.AddRange(
            new Character("Philip J. Fry") { Actor = billyWest, Species = "Human", Planet = "Earth" },
            new Character("Turanga Leela") { Actor = kateySagal, Species = "Mutant, Human", Planet = "Earth" },
            new Character("Bender Bending Rodriquez") { Actor = johnDiMaggio, Species = "Robot", Planet = "Tijuana, Baja California" },
            new Character("John A. Zoidberg") { Actor = billyWest, Species = "Decapodian", Planet = "Decapod 10" },
            new Character("Amy Wong") { Actor = laurenTom, Species = "Human", Planet = "Mars" },
            new Character("Hermes Conrad") { Actor = philLaMarr, Species = "Human", Planet = "Earth" },
            new Character("Hubert J. Farnsworth") { Actor = billyWest, Species = "Human", Planet = "Earth" }
        );

        context.SaveChanges();
    }
}
