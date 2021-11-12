foreach (var number in Fibonacci())
{
    Console.WriteLine(number);
}

foreach (var number in StreamNumbers())
{
    Console.WriteLine(number);
}

var ducks = CollectionUtilities.ToDictionary(Duck.Ducks);

Console.WriteLine(ducks["Donald Duck"]);

static IEnumerable<int> StreamNumbers()
{
    int i = 0;
    while (true)
    {
        yield return i++;
    }
}

static IEnumerable<long> Fibonacci()
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