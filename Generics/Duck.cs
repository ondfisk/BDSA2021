namespace Generics;

public class Duck : IEquatable<Duck>, IComparable<Duck>
{
    public int Id { get; }

    public string Name { get; }

    public int Age { get; }

    public Duck(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    /// <summary>
    /// Two ducks are considered equal if they have the same name
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Duck? other)
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
    public override bool Equals(object? obj) => Equals(obj as Duck);

    public static bool operator ==(Duck? x, Duck? y)
    {
        return x?.Name == y?.Name;
    }

    public static bool operator !=(Duck? x, Duck? y)
    {
        return x?.Name != y?.Name;
    }

    public int CompareTo(Duck? other)
    {
        if (other == null)
        {
            return 1;
        }
        if (Age > other.Age)
        {
            return 1;
        }
        if (Age < other.Age)
        {
            return -1;
        }


        return 0;
    }

    /// <summary>
    /// GetHashCode should always be overridden when Equals is overridden.
    /// https://docs.microsoft.com/en-us/visualstudio/code-quality/ca2218-override-gethashcode-on-overriding-equals
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public override string ToString() => $"{Id}: {Name}, {Age}";

    public static ICollection<Duck> Ducks { get; } = new[] {
            new Duck(3, "Huey Duck", 10),
            new Duck(8, "Magica De Spell", 302),
            new Duck(4, "Dewey Duck", 10),
            new Duck(5, "Louie  Duck", 10),
            new Duck(6, "Scrooge McDuck", 60),
            new Duck(7, "Flintheart Glomgold", 66),
            new Duck(1, "Donald Duck", 32),
            new Duck(2, "Daisy Duck", 30),
            new Duck(9, "John D. Rockerduck", 55)
        };
}
