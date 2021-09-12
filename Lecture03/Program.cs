using System;
using Lecture03.Models;

namespace Lecture03
{
    public delegate int BinaryOperation(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            var repo = new Repository();
            foreach (var hero in repo.Superheroes2)
            {
                Console.WriteLine(hero);
            }
        }
    }
}
