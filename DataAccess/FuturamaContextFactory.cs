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
        context.Database.ExecuteSqlRaw("DELETE dbo.Characters");
        context.Database.ExecuteSqlRaw("DELETE dbo.Actors");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Characters', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Actors', RESEED, 0)");

        var billyWest = new Actor { Name = "Billy West" };
        var kateySagal = new Actor { Name = "Katey Sagal" };
        var johnDiMaggio = new Actor { Name = "John DiMaggio" };
        var laurenTom = new Actor { Name = "Lauren Tom" };
        var philLaMarr = new Actor { Name = "Phil LaMarr" };

        context.Characters.AddRange(
            new Character { Actor = billyWest, Name = "Philip J. Fry", Species = "Human", Planet = "Earth" },
            new Character { Actor = kateySagal, Name = "Turanga Leela", Species = "Mutant, Human", Planet = "Earth" },
            new Character { Actor = johnDiMaggio, Name = "Bender Bending Rodriquez", Species = "Robot", Planet = "Tijuana, Baja California" },
            new Character { Actor = billyWest, Name = "John A. Zoidberg", Species = "Decapodian", Planet = "Decapod 10" },
            new Character { Actor = laurenTom, Name = "Amy Wong", Species = "Human", Planet = "Mars" },
            new Character { Actor = philLaMarr, Name = "Hermes Conrad", Species = "Human", Planet = "Earth" },
            new Character { Actor = billyWest, Name = "Hubert J. Farnsworth", Species = "Human", Planet = "Earth" }
        );

        context.SaveChanges();
    }
}
