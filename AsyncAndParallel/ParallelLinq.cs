namespace AsyncAndParallel;

public class ParallelLinq
{
    public static void Run()
    {
        var numbers = Enumerable.Range(1, 5000000);

        var query = from n in numbers.AsParallel().AsOrdered()
                    where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
                    select n;

        var primes = Time(query.ToArray, out TimeSpan time);

        Console.WriteLine("Primes: {0}, first: {1}, last: {2}", time, primes.First(), primes.Last());
    }

    private static T Time<T>(Func<T> action, out TimeSpan time)
    {
        var stopwatch = Stopwatch.StartNew();

        var result = action();

        stopwatch.Stop();

        time = stopwatch.Elapsed;

        return result;
    }
}
