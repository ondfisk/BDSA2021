namespace AsyncAndParallel;

public class ConcurrentCollections
{
    static void Race(ConcurrentQueue<string> queue, string name, int count)
    {
        for (var i = 0; i < count; i++)
        {
            queue.Enqueue($"{name}: {i}");
        }
    }

    public static void Race()
    {
        var queue = new ConcurrentQueue<string>();

        var t1 = Task.Run(() => Race(queue, "One", 50));
        var t2 = Task.Run(() => Race(queue, "Two", 50));
        var t3 = Task.Run(() => Race(queue, "Three", 50));
        var t4 = Task.Run(() => Race(queue, "Four", 50));
        var t5 = Task.Run(() => Race(queue, "Five", 50));

        Task.WaitAll(t1, t2, t3, t4, t5);

        queue.ToList().ForEach(Console.WriteLine);
    }
}
