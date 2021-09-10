using System;
using System.Collections.Generic;

namespace Lecture02
{
    public class Duck : IEquatable<Duck>, IComparable<Duck>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// Two ducks are considered equal if they have the same name
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Duck other)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                return Name == other.Name;
            }
        }

        /// <summary>
        /// Overridden default equals to compare ducks by Name
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => Equals(obj as Duck);

        public static bool operator ==(Duck x, Duck y)
        {
            return x.Name == y.Name;
        }

        public static bool operator !=(Duck x, Duck y)
        {
            return x.Name != y.Name;
        }

        public int CompareTo(Duck other)
        {
            if (Age < other.Age)
            {
                return -1;
            }
            if (Age > other.Age)
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// GetHashCode should always be overridden when Equals is overridden.
        /// https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2218-override-gethashcode-on-overriding-equals
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => $"{Id}: {Name}, {Age}";

        public static ICollection<Duck> Ducks = new[] {
            new Duck { Id = 3, Name = "Huey Duck", Age = 10 },
            new Duck { Id = 8, Name = "Magica De Spell", Age = 302 },
            new Duck { Id = 4, Name = "Dewey Duck", Age = 10 },
            new Duck { Id = 5, Name = "Louie  Duck", Age = 10 },
            new Duck { Id = 6, Name = "Scrooge McDuck", Age = 60 },
            new Duck { Id = 7, Name = "Flintheart Glomgold", Age = 66 },
            new Duck { Id = 1, Name = "Donald Duck", Age = 32 },
            new Duck { Id = 2, Name = "Daisy Duck", Age = 30 },
            new Duck { Id = 9, Name = "John D. Rockerduck", Age = 55 }
        };
    }
}
