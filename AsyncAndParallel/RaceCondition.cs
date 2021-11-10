namespace AsyncAndParallel;

public class RaceCondition
{
    private static Random _random = new Random();

    public static void Race(Queue<string> sb, string name, int count)
    {
        for (var i = 0; i < count; i++)
        {
            // Thread.Sleep(_random.Next(5));
            sb.Enqueue($"{name}: {i}");
        }
    }

    public static void Race()
    {
        var sb = new Queue<string>();
        var t1 = new Thread(() => Race(sb, "One", 50));
        var t2 = new Thread(() => Race(sb, "Two", 50));
        var t3 = new Thread(() => Race(sb, "Three", 50));
        var t4 = new Thread(() => Race(sb, "Four", 50));
        var t5 = new Thread(() => Race(sb, "Five", 50));
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();
        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
        t5.Join();
        // sb.ForEach(Console.WriteLine);
        // Console.WriteLine(sb);
    }
}

public class FixedRace
{
    private static Random _random = new Random();

    public static void Race(StringBuilder sb, string name, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Thread.Sleep(_random.Next(5));
            lock (sb)
            {
                sb.AppendLine($"{name}: {i}");
            }
        }
    }

    public static void Race()
    {
        var sb = new StringBuilder();
        var t1 = new Thread(() => Race(sb, "One", 50));
        var t2 = new Thread(() => Race(sb, "Two", 50));
        var t3 = new Thread(() => Race(sb, "Three", 50));
        var t4 = new Thread(() => Race(sb, "Four", 50));
        var t5 = new Thread(() => Race(sb, "Five", 50));
        t1.Start();
        t2.Start();
        t3.Start();
        t4.Start();
        t5.Start();
        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
        t5.Join();
        Console.WriteLine(sb);
    }
}

public class BehindTheScenes
{
    public static void Race(StringBuilder sb, string name, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var lockAquired = false;
            try
            {
                Monitor.Enter(sb, ref lockAquired);
                sb.AppendLine($"{name}: {i}");
            }
            finally
            {
                if (lockAquired)
                {
                    Monitor.Exit(sb);
                }
            }
        }
    }

    public static void Race()
    {
        var sb = new StringBuilder();
        var t1 = new Thread(() => Race(sb, "One", 50));
        var t2 = new Thread(() => Race(sb, "Two", 50));
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        Console.WriteLine(sb);
    }
}
