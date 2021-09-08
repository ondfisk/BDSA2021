using System;
using System.Collections.Generic;

namespace Lecture02
{
    class Program
    {
        static void Main(string[] args)
        {
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
