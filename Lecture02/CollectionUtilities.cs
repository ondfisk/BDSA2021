using System;
using System.Collections.Generic;

namespace Lecture02
{
    public static class CollectionUtilities
    {
        public static IEnumerable<int> GetEven(IEnumerable<int> list)
        {
            foreach (var number in list)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }

        public static bool Find(IEnumerable<int> numbers, int number)
        {
            foreach (var n in numbers)
            {
                if (n == number)
                {
                    return true;
                }
            }
            return false;
        }

        public static ISet<int> Unique(IEnumerable<int> numbers) => new HashSet<int>(numbers);

        public static IEnumerable<int> Reverse(IEnumerable<int> numbers) => new Stack<int>(numbers);

        public static void Sort(List<Duck> ducks, IComparer<Duck> comparer = null)
        {
            ducks.Sort(comparer);
        }

        public static IDictionary<string, Duck> ToDictionary(IEnumerable<Duck> ducks)
        {
            var dict = new Dictionary<string, Duck>();
            foreach (var duck in ducks)
            {
                dict.Add(duck.Name, duck);
            }

            return dict;
        }

        public static IEnumerable<Duck> GetOlderThan(IEnumerable<Duck> ducks, int age)
        {
            throw new NotImplementedException();
        }
    }
}
