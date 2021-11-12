namespace Linq;

public static class Extensions
{
    public static void Print<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public static void PrintSingle<T>(this T item) => Console.WriteLine(item);
}
