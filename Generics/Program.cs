using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            // foreach (var number in Fibonacci())
            // {
            //     Console.WriteLine(number);
            // }

            var ducks = CollectionUtilities.ToDictionary(Duck.Ducks);

            Console.WriteLine(ducks["Donald Duck"]);
        }

        public static IEnumerable<int> StreamNumbers()
        {
            int i = 0;
            while (true)
            {
                yield return i++;
            }
        }

        public static IEnumerable<long> Fibonacci()
        {
            var a = 1L;
            var b = 0L;

            while (true)
            {
                var c = a + b;

                a = b;
                b = c;

                if (c < 0)
                {
                    yield break;
                }

                yield return c;
            }
        }
    }
}
