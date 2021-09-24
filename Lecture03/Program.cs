using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Lecture03.Models;
using static System.Console;
using System.Linq;

namespace Lecture03
{
    // public delegate int BinaryOperation(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            // var add = (int x, int y) => x + y;

            // var results = Compute(4, 2, add, Subtract, (x, y) => x * y);

            // foreach (var result in results)
            // {
            //     WriteLine(result);
            // }

            // var system = new SubSystem((i, l) => WriteLine($"{l}: {i}"));

            // system.Operation("Show me the LOGS!!!!!");

            var repo = new Repository();
            var superheroes = repo.Superheroes;

            // superheroes.Where(s => s.City == "Gotham City").Print();

            // superheroes.Any(s => s.GivenName.Contains("a")).Print();

            // new[] { 1, 2, 3, 4, 4, 3, 3, 3 }.Distinct().Print();

            // var older = superheroes.Where(s => s.FirstAppearance < 1950);

            // var females = older.Where(s => s.Gender == Gender.Female);

            // var sort = older.OrderBy(s => s.FirstAppearance).ThenBy(s => s.Name).Select(s => new {
            //     FullName = s.Name,
            //     s.FirstAppearance,
            // });

            // sort.Print();

            var cities = from c in repo.Cities
                         where c.Name.StartsWith("A")
                         select c.Name;

            // cities.Print();

            var h1 = from h in repo.Superheroes
                     join c in repo.Cities on h.CityId equals c.Id
                     select new { h.AlterEgo, City = c.Name };

            // h1.Print();

            var gr = from h in repo.Superheroes
                     join c in repo.Cities on h.CityId equals c.Id
                     group (h, c) by c.Name into g
                     select new
                     {
                         City = g.Key,
                         Count = g.Count()
                     };

            // gr.Print();

            var gr2 = from h in h1
                      group h by h.City into g
                      select new
                      {
                          City = g.Key,
                          Count = g.Count()
                      };

            // gr2.Print();

            var h3 = repo.Superheroes
                         .Where(h => h.FirstAppearance < 1950)
                         .Join(repo.Cities, h => h.CityId, c => c.Id, (h, c) => (h.AlterEgo, c.Name));

            // h3.Print();

            var first3 = repo.Superheroes.Take(3);

            var h4 = (from h in repo.Superheroes
                      let x = h.AlterEgo
                      join c in repo.Cities on h.CityId equals c.Id
                      orderby c.Name, h.AlterEgo descending
                      select new { h.AlterEgo, City = c.Name }).Take(3);


            // repo.Superheroes.SingleOrDefault(s => s.FirstAppearance > 2020).PrintSingle();

            var text = File.ReadAllText("Hamlet.txt");

            var words = Regex.Split(text, @"\P{L}+");

            var histogram = from w in words
                            group w by w into h
                            let c = h.Count()
                            orderby c descending
                            select new { Word = h.Key, Count = c };

            histogram.Take(5).Print();
        }

        static int Subtract(int x, int y)
        {
            return x - y;
        }

        static IEnumerable<(int, int, int)> Compute(int x, int y, params Func<int, int, int>[] operations)
        {
            foreach (var operation in operations)
            {
                yield return (x, y, operation(x, y));
            }
        }

        #region Local Functions
        public static int LocalFunctionFactorial(int n)
        {
            return nthFactorial(n);

            int nthFactorial(int number) => number < 2
                ? 1
                : number * nthFactorial(number - 1);
        }

        public static int LambdaFactorial(int n)
        {
            Func<int, int> nthFactorial = default(Func<int, int>);

            nthFactorial = number => number < 2
                ? 1
                : number * nthFactorial(number - 1);

            return nthFactorial(n);
        }
        #endregion
    }
}
