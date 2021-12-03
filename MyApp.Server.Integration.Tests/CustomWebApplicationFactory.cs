

namespace MyApp.Server.Integration.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ComicsContext>));

            if (dbContext != null)
            {
                services.Remove(dbContext);
            }

            /* Overriding policies and adding Test Scheme defined in TestAuthHandler */
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Test")
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
                options.DefaultScheme = "Test";
            })
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

            var connection = new SqliteConnection("Filename=:memory:");

            services.AddDbContext<ComicsContext>(options =>
            {
                options.UseSqlite(connection);
            });

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<ComicsContext>();
            appContext.Database.OpenConnection();
            appContext.Database.EnsureCreated();

            Seed(appContext);
        });

        builder.UseEnvironment("Integration");

        return base.CreateHost(builder);
    }

    private void Seed(ComicsContext context)
    {
        var metropolis = new City("Metropolis");
        var gothamCity = new City("Gotham City");
        var themyscira = new City("Themyscira");

        var superStrength = new Power("super strength");
        var flight = new Power("flight");
        var invulnerability = new Power("invulnerability");
        var superSpeed = new Power("super speed");
        var heatVision = new Power("heat vision");
        var freezeBreath = new Power("freeze breath");
        var xRayVision = new Power("x-ray vision");
        var superhumanHearing = new Power("superhuman hearing");
        var healingFactor = new Power("healing factor");
        var exceptionalMartialArtist = new Power("exceptional martial artist");
        var combatStrategy = new Power("combat strategy");
        var inexhaustibleWealth = new Power("inexhaustible wealth");
        var brilliantDeductiveSkill = new Power("brilliant deductive skill");
        var advancedTechnology = new Power("advanced technology");
        var combatSkill = new Power("combat skill");
        var superhumanAgility = new Power("superhuman weaponry");
        var magicWeaponry = new Power("magic agility");
        var gymnasticAbility = new Power("gymnastic ability");

        context.Characters.AddRange(
            new Character { Id = 1, GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = 1938, ImageUrl = "https://images.com/superman.png", Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
            new Character { Id = 2, GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = 1939, ImageUrl = "https://images.com/batman.png", Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
            new Character { Id = 3, GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = 1941, ImageUrl = "https://images.com/wonder-woman.png", Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
            new Character { Id = 4, GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = 1940, ImageUrl = "https://images.com/catwoman.png", Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
        );

        context.SaveChanges();
    }
}
