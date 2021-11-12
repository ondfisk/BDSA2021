#nullable disable
namespace AsyncAndParallel;

public class Tasks
{
    private static void Race(StringBuilder sb, string name, int count)
    {
        for (var i = 0; i < count; i++)
        {
            Task.Delay(TimeSpan.FromMilliseconds(20)).Wait();
            lock (sb)
            {
                sb.AppendLine($"{name}: {i}");
            }
        }
    }

    public static void TaskFactory()
    {
        var sb = new StringBuilder();
        var t1 = Task.Factory.StartNew(() => Race(sb, "One", 50));
        var t2 = Task.Run(() => Race(sb, "Two", 50));

        Console.WriteLine(sb);
    }

    public static void Wait()
    {
        var sb = new StringBuilder();
        var t1 = Task.Factory.StartNew(() => Race(sb, "One", 50));
        var t2 = Task.Run(() => Race(sb, "Two", 50));

        t1.Wait();
        t2.Wait();

        Console.WriteLine(sb);
    }

    public void DoALotOfStuff(params Task[] tasks)
    {
        // foreach (var t in tasks)
        // {
        //     t.Start();
        // }

        Task.WaitAll(tasks);
    }

    public void CallDoALotOfStuff()
    {
        var t0 = Task.Run(() => Console.Write("My task no. 0"));
        var t1 = Task.Run(() => Console.Write("My task no. 1"));
        var t2 = Task.Run(() => Console.Write("My task no. 2"));
        var t3 = Task.Run(() => Console.Write("My task no. 3"));

        DoALotOfStuff(t0, t1, t2, t3);
    }

    public static void WaitAll()
    {
        var sb = new StringBuilder();
        var t1 = Task.Factory.StartNew(() => Race(sb, "One", 50));
        var t2 = Task.Run(() => Race(sb, "Two", 50));

        Task.WaitAll(t1, t2);

        Console.WriteLine(sb);
    }

    public static void Attached()
    {
        var sb = new StringBuilder();

        var t = Task.Run(() =>
        {
            Task.Factory.StartNew(() => Race(sb, "One", 50), TaskCreationOptions.AttachedToParent).Wait();
            Task.Factory.StartNew(() => Race(sb, "Two", 50), TaskCreationOptions.AttachedToParent).Wait();
        });
        t.Wait();

        Console.WriteLine(sb);
    }

    public static void Continuation()
    {
        var sb = new StringBuilder();

        var t1 = Task.Run(() => Race(sb, "One", 5))
                     .ContinueWith(t2 => Race(sb, "Two", 5))
                     .ContinueWith(t3 => Race(sb, "Three", 5));

        t1.Wait();

        Console.WriteLine(sb);
    }

    public static void Result()
    {
        Task<int> t = Task.Run(() =>
        {
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
            var factorial = 1;
            for (var i = 2; i <= 6; i++)
            {
                factorial *= i;
            }
            return factorial;
        });

        Console.WriteLine("Task started");
        Console.WriteLine(t.Result); // <= Blocking! Waits for task to finish
    }

    public static void Cancellation()
    {
        var sb = new StringBuilder();
        var tokenSource = new CancellationTokenSource();

        var t1 = Task.Factory.StartNew(() => Race(sb, "One", 50, tokenSource.Token));
        var t2 = Task.Run(() => Race(sb, "Two", 50, tokenSource.Token));

        Task.Delay(20).Wait();
        tokenSource.Cancel();

        Task.WaitAll(t1, t2);

        Console.WriteLine(sb);
    }

    private static void Race(StringBuilder sb, string name, int count, CancellationToken token)
    {
        for (var i = 0; i < count && !token.IsCancellationRequested; i++)
        {
            lock (sb)
            {
                sb.AppendLine($"{name}: {i}");
            }
            Thread.Sleep(2);
        }
        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Cancelled!!!");
        }
    }

    public static void ResultCancelled()
    {
        var tokenSource = new CancellationTokenSource();

        var t = Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            var factorial = 1;
            for (var i = 2; i <= 6; i++)
            {
                factorial *= i;
            }
            return factorial;
        }, tokenSource.Token);

        tokenSource.Cancel();

        Console.WriteLine("Task started");
        Console.WriteLine(t.Result); // <= Blocking! Waits for task to finish
    }

    public static void Fail()
    {
        var z = 0;

        var f0 = Task.Run(() => Console.WriteLine("f0 ok"));
        var f1 = Task.Run(() => string.Join(null, null));
        var f2 = Task.Run(() => 42 / z);
        var f3 = Task.Run(() => { throw new Exception("EPIC FAIL!!!"); });
        var f4 = Task.Run(() => Console.WriteLine("f4 ok"));
        var f5 = Task.Run(() => Console.WriteLine("f5 ok"));

        Task.WaitAll(f0, f1, f2, f3, f4, f5);
    }
}
