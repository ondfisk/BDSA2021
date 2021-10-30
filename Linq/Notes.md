# Notes

## LINQ

```csharp
var teams = from g in repo.Groups
            select new HashSet<Superhero>(
                from h in repo.Superheroes
                where h.GroupAffiliations.Contains(g)
                select h
            );
```

## Extension

```csharp
public static void ToString<T>(this T stuff)
{
    Console.WriteLine(stuff);
}

public static void Print<T>(this T stuff, int depth = 0)
{
    if (stuff is IEnumerable<int> numbers)
    {
        Console.WriteLine(string.Join(", ", numbers));
    }
    else if (stuff is IEnumerable items)
    {
        foreach (var item in items)
        {
            Print(item, depth + 1);
        }
    }
    else
    {
        Console.WriteLine($"{string.Concat(Enumerable.Repeat('-', depth))} {stuff}");
    }
}

public static void Print<T>(this T stuff, int depth = 0)
{
    switch (stuff)
    {
        case IEnumerable<int> numbers:
            Console.WriteLine(string.Join(", ", numbers));
            break;
        case IEnumerable items:
            foreach (var item in items)
            {
                Print(item, depth + 1);
            }
            break;
        default:
            Console.WriteLine($"{string.Concat(Enumerable.Repeat('-', depth))} {stuff}");
            break;
    }
}
```

## Hamlet

```csharp
public static void Hamlet()
{
    var text = File.ReadAllText("Hamlet.txt");

    var words = Regex.Split(text, @"\P{L}+");

    var histogram = from w in words
                    group w by w into h
                    let c = h.Count()
                    orderby c descending
                    select new { Word = h.Key, Count = c };

    ToString(histogram.Take(5));

    // Dictionary
    // LO
}
```
