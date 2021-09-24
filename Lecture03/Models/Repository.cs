using System;
using System.Collections.Generic;
using System.Linq;
using static Lecture03.Models.Gender;

namespace Lecture03.Models
{
    public class Repository
    {
        public ICollection<Superhero> Superheroes { get; }
        public ICollection<Superhero2> Superheroes2 { get; }
        public ICollection<Group> Groups { get; }
        public ICollection<City> Cities { get; }

        public Repository()
        {
            Groups = new HashSet<Group>
            {
                new Group { Id = 1, Name = "Justice League" },
                new Group { Id = 2, Name = "The Others" },
                new Group { Id = 3, Name = "Justice Society" },
                new Group { Id = 4, Name = "The Outsiders" },
                new Group { Id = 5, Name = "Batman, Incorporated" }
            };

            HashSet<Group> getGroups(params int[] ids) => Groups.Where(g => ids.Contains(g.Id)).ToHashSet();

            Cities = new HashSet<City>
            {
                new City(1, "Metropolis"),
                new City(2, "Gotham City"),
                new City(3, "Atlantis"),
                new City(4, "Themyscira"),
                new City(5, "New York City"),
                new City(6, "Central City")
            };

            Superheroes = new HashSet<Superhero>
            {
                new Superhero { Id = 1, Name = "Arthur Curry", AlterEgo = "Aquaman", Occupation = "King of Atlantis", Gender = Male, FirstAppearance = 1941, Powers = new[] { "super strength", "durability", "control over sea life", "exceptional swimming ability", "ability to breathe underwater" }, GroupAffiliations = getGroups(1, 2), CityId = 1 },
                new Superhero { Id = 2, Name = "Clark Kent", AlterEgo = "Superman", Occupation = "Reporter", Gender = Male, FirstAppearance = 1938, Powers = new[] { "super strength", "flight", "invulnerability", "super speed", "heat vision", "freeze breath", "x-ray vision", "superhuman hearing", "healing factor" }, GroupAffiliations = getGroups(1, 3), CityId = 1 },
                new Superhero { Id = 3, Name = "Bruce Wayne", AlterEgo = "Batman", Occupation = "CEO of Wayne Enterprises", Gender = Male, FirstAppearance = 1939, Powers = new[] { "exceptional martial artist", "combat strategy", "inexhaustible wealth", "brilliant deductive skill", "advanced technology" }, GroupAffiliations = getGroups(1, 3, 4, 5), CityId = 2 },
                new Superhero { Id = 4, Name = "Diana", AlterEgo = "Wonder Woman", Occupation = "Amazon Princess", Gender = Female, FirstAppearance = 1941, Powers = new[] { "super strength", "invulnerability", "flight", "combat skill", "combat strategy", "superhuman agility", "healing factor", "magic weaponry" }, GroupAffiliations = getGroups(1), CityId = 4 },
                new Superhero { Id = 5, Name = "Hal Jordan", AlterEgo = "Green Lantern", Occupation = "Test pilot", Gender = Male, FirstAppearance = 1940, Powers = new[] { "hard light constructs", "instant weaponry", "force fields", "flight", "durability", "alien technology" }, GroupAffiliations = getGroups(1, 3), CityId = 5 },
                new Superhero { Id = 6, Name = "Barry Allen", AlterEgo = "The Flash", Occupation = "Forensic scientist", Gender = Male, FirstAppearance = 1940, Powers = new[] { "super speed", "intangibility", "superhuman agility" }, GroupAffiliations = getGroups(1, 3), CityId = 6 },
                new Superhero { Id = 7, Name = "Selina Kyle", AlterEgo = "Catwoman", Occupation = "Thief", Gender = Female, FirstAppearance = 1940, Powers = new[] { "exceptional martial artist", "gymnastic ability", "combat skill" }, GroupAffiliations = getGroups(), CityId = 2 },
                new Superhero { Id = 8, Name = "Kate Kane", AlterEgo = "Batwoman", Occupation = "Thief", Gender = Female, FirstAppearance = 1956, Powers = new[] { "exceptional martial artist", "combat strategy", "combat skill", "brilliant deductive skills", "intelligence", "advanced technology" }, GroupAffiliations = getGroups(), CityId = 2 },
                new Superhero { Id = 9, Name = "Kara Zor-El", AlterEgo = "Supergirl", Occupation = "Actress", Gender = Female, FirstAppearance = 1959, Powers = new[] { "super strength", "flight", "invulnerability", "super speed", "heat vision", "freeze breath", "x-ray vision", "superhuman hearing", "healing factor" }, GroupAffiliations = getGroups(), CityId = 5 }
            };

            Superheroes2 = new HashSet<Superhero2>
            {
                new Superhero2 { GivenName = "Clark", Surname = "Kent", AlterEgo = "Superman", FirstAppearance = DateTime.Parse("1938-04-18"), City = "Metropolis" },
                new Superhero2 { GivenName = "Bruce", Surname = "Wayne", AlterEgo = "Batman", FirstAppearance = DateTime.Parse("1939-05-01"), City = "Gotham City" },
                new Superhero2 { GivenName = "Bruce", Surname = "Banner", AlterEgo = "Hulk", FirstAppearance = DateTime.Parse("1962-05-01"), City = "Dayton" },
                new Superhero2 { GivenName = "Steve", Surname = "Rogers", AlterEgo = "Captain America", FirstAppearance = DateTime.Parse("1941-03-01"), City = "New York City" },
                new Superhero2 { GivenName = "Tony", Surname = "Stark", AlterEgo = "Iron Man", FirstAppearance = DateTime.Parse("1963-03-01"), City = "Long Island" },
                new Superhero2 { GivenName = "James", Surname = "Howlett", AlterEgo = "Wolverine", FirstAppearance = DateTime.Parse("1974-10-01"), City = "Cold Lake" },
                new Superhero2 { GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", FirstAppearance = DateTime.Parse("1940-04-01"), City = "Gotham City" }
            };
        }
    }
}
