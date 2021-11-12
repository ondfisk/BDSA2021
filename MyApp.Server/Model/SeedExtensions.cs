namespace MyApp.Server.Model;

public static class SeedExtensions
{
    public static IHost Seed(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ComicsContext>();

            SeedCharacters(context);
        }
        return host;
    }

    private static void SeedCharacters(ComicsContext context)
    {
        context.Database.Migrate();

        if (!context.Characters.Any())
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
                new Character { GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = 1938, Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
                new Character { GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = 1939, Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
                new Character { GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = 1941, Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
                new Character { GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = 1940, Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
            );

            context.SaveChanges();
        }
    }
}
