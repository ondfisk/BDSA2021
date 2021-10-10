using System;
using System.Text;
using System.Threading;

namespace Lecture06
{
    public class RaceCondition
    {
        public static void Race(StringBuilder sb, string name, int count)
        {
            for (var i = 0; i < count; i++)
            {
                Thread.Sleep(2);
                sb.AppendLine($"{name}: {i}");
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

    public class FixedRace
    {
        public static void Race(StringBuilder sb, string name, int count)
        {
            for (var i = 0; i < count; i++)
            {
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
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
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
}
