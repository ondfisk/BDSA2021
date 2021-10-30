using System;
using System.Linq;
using MyApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static MyApp.Core.Gender;

namespace MyApp.Api.Model
{
    public static class SeedExtensions
    {
        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ComicsContext>();

                SeedCharacters(context);
            }
            return host;
        }

        private static void SeedCharacters(ComicsContext context)
        {
            context.Database.Migrate();

            if (!context.Characters.Any())
            {
                var metropolis = new City { Name = "Metropolis" };
                var gothamCity = new City { Name = "Gotham City" };
                var themyscira = new City { Name = "Themyscira" };

                var superStrength = new Power { Name = "super strength" };
                var flight = new Power { Name = "flight" };
                var invulnerability = new Power { Name = "invulnerability" };
                var superSpeed = new Power { Name = "super speed" };
                var heatVision = new Power { Name = "heat vision" };
                var freezeBreath = new Power { Name = "freeze breath" };
                var xRayVision = new Power { Name = "x-ray vision" };
                var superhumanHearing = new Power { Name = "superhuman hearing" };
                var healingFactor = new Power { Name = "healing factor" };
                var exceptionalMartialArtist = new Power { Name = "exceptional martial artist" };
                var combatStrategy = new Power { Name = "combat strategy" };
                var inexhaustibleWealth = new Power { Name = "inexhaustible wealth" };
                var brilliantDeductiveSkill = new Power { Name = "brilliant deductive skill" };
                var advancedTechnology = new Power { Name = "advanced technology" };
                var combatSkill = new Power { Name = "combat skill" };
                var superhumanAgility = new Power { Name = "superhuman weaponry" };
                var magicWeaponry = new Power { Name = "magic agility" };
                var gymnasticAbility = new Power { Name = "gymnastic ability" };

                context.Characters.AddRange(
                    new Character { GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", Occupation = "Reporter", City = metropolis, Gender = Male, FirstAppearance = DateTime.Parse("1938-04-18"), Powers = new[] { superStrength, flight, invulnerability, superSpeed, heatVision, freezeBreath, xRayVision, superhumanHearing, healingFactor } },
                    new Character { GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", City = gothamCity, Gender = Male, FirstAppearance = DateTime.Parse("1939-05-01"), Powers = new[] { exceptionalMartialArtist, combatStrategy, inexhaustibleWealth, brilliantDeductiveSkill, advancedTechnology } },
                    new Character { GivenName = "Diana", Surname = "Prince", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", City = themyscira, Gender = Female, FirstAppearance = DateTime.Parse("1941-10-21"), Powers = new[] { superStrength, invulnerability, flight, combatSkill, combatStrategy, superhumanAgility, healingFactor, magicWeaponry } },
                    new Character { GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = DateTime.Parse("1940-04-01"), Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
                );

                context.SaveChanges();
            }
        }
    }
}
