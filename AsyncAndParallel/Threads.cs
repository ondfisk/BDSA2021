namespace AsyncAndParallel;

public class Threads
{
    /// <summary>
    /// Run an open Process Explorer...
    /// </summary>
    public static void SpawnThread()
    {
        var thread = new Thread(() =>
        {
            Console.WriteLine("I'm a thread");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("Thread done");
        });
        thread.Start();
    }

    /// <summary>
    /// Run an open Process Explorer...
    /// </summary>
    /// <param name="number"></param>
    public static void SpawnMultipleThreads(int numberOfThreads)
    {
        Console.WriteLine("Hello Threads");
        Console.WriteLine($"Press any key to spawn {numberOfThreads} threads");
        Console.ReadKey();

        var random = new Random();

        var t = new Thread[numberOfThreads];
        for (var i = 0; i < t.Length; i++)
        {
            var n = i + 1;
            var duration = random.Next(10) + 5;
            t[i] = new Thread(() =>
            {
                Console.WriteLine("I'm thread no. {0}", n);
                Thread.Sleep(TimeSpan.FromSeconds(duration));
                Console.WriteLine("Thread no. {0} completed", n);
            });
            t[i].Start();
        }
    }

    public static void Overlapping()
    {
        var t = new Thread(WriteX);
        t.Start();

        for (var i = 0; i < 400; i++)
        {
            Console.Write('Y');
        }
    }

    private static void WriteX()
    {
        for (var i = 0; i < 400; i++)
        {
            Console.Write('X');
        }
    }

    private static void Write(char c, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Console.Write(c);
        }
        Console.WriteLine();
    }

    private static void Write(object? c)
    {
        Thread.Sleep(2000);
        if (c is char ch)
        {
            Write(ch, 40);
        }
    }

    public static void OverlappingWithArguments()
    {
        var t1 = new Thread(WriteX);
        t1.Start();

        var t2 = new Thread(Write);
        t2.Start('Z');

        var t3 = new Thread(() => Write('Y', 40));
        t3.Start();
    }

    public static void Join()
    {
        var t1 = new Thread(
            () =>
            {
                for (var i = 0; i < 100; i++)
                {
                    Console.WriteLine($"Hello {i}");
                }
            })
        {
            IsBackground = true
        };

        t1.Start();
        t1.Join();
        Console.WriteLine("Thread t has ended");
        Console.WriteLine("MAIN!!!");
        Console.WriteLine("Done waiting...");
    }
}
