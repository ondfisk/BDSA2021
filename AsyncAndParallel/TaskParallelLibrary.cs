namespace AsyncAndParallel;

public class TaskParallelLibrary
{
    public static void For()
    {
        Parallel.For(0, 999, i =>
        {
            Console.WriteLine(i);
        });
    }

    public static void ForEach()
    {
        var sw = Stopwatch.StartNew();

        var numbers = Enumerable.Range(0, 1000);

        var options = new ParallelOptions { MaxDegreeOfParallelism = 1 };

        Parallel.ForEach(numbers, number =>
        {
            Task.Delay(1).Wait();
            Console.WriteLine(number);
        });

        Console.WriteLine($"That took: {sw.ElapsedMilliseconds} milis");
    }

    public static void Invoke()
    {
        var sw = Stopwatch.StartNew();

        Parallel.Invoke(
            SuperLongRunningThingy1,
            SuperLongRunningThingy2,
            SuperLongRunningThingy3,
            SuperLongRunningThingy1,
            SuperLongRunningThingy2,
            SuperLongRunningThingy3,
            SuperLongRunningThingy4
        );

        sw.Stop();

        Console.WriteLine(sw.Elapsed);
    }

    private static void SuperLongRunningThingy1()
    {
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }

    private static void SuperLongRunningThingy2()
    {
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }

    private static void SuperLongRunningThingy3()
    {
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }

    private static void SuperLongRunningThingy4()
    {
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }
}
