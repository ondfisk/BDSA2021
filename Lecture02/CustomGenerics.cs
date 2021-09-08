using System;
using System.Collections.Generic;

namespace Lecture02
{
    public interface IPriorityQueue<T> // where T : IComparable<T>
    {
        int Count { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        void Clear();
        bool Contains(T item);
    }
}
