namespace MyApp.Server.Model;

public static class SeedExtensions
{
    public static async Task<IHost> SeedAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ComicsContext>();
            var repository = scope.ServiceProvider.GetRequiredService<IImageRepository>();

            await SeedCharactersAsync(context, repository);
        }
        return host;
    }

    private static async Task SeedCharactersAsync(ComicsContext context, IImageRepository repository)
    {
        await context.Database.MigrateAsync();

        if (!await context.Characters.AnyAsync())
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
                new Character { GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = 1938, ImageUrl = await UploadAsync(repository, "superman.png", "b30e255d-dd71-49ba-8e43-ad93f86904a3"), Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
                new Character { GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = 1939, ImageUrl = await UploadAsync(repository, "batman.jpg", "99455db8-9288-466d-8ba9-e93543df8939"), Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
                new Character { GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = 1941, ImageUrl = await UploadAsync(repository, "wonder-woman.jpg", "b146fa1b-d485-4ed4-b6b5-ddb9d71ec1b9"), Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
                new Character { GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = 1940, ImageUrl = await UploadAsync(repository, "catwoman.jpg", "9b8bb013-154f-431c-afb7-946aabeaee78"), Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
            );

            await context.SaveChangesAsync();
        }
    }

    private static async Task<string> UploadAsync(IImageRepository repository, string image, string name)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Data", image);
        var extension = Path.GetExtension(path);
        var contentType = extension switch
        {
            ".jpg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream"
        };

        using var stream = File.OpenRead(path);

        var (_, uri) = await repository.CreateImageAsync(name, contentType, stream);

        return uri.ToString();
    }
}
