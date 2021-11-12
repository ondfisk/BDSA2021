namespace Generics;

public interface IPriorityQueue<T> // where T : IComparable<T>
{
    int Count { get; }
    void Enqueue(T item);
    T Dequeue();
    T Peek();
    void Clear();
    bool Contains(T item);
}

public class PriorityQueue<T> : IPriorityQueue<T>
{
    public int Count => throw new NotImplementedException();

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new NotImplementedException();
    }

    public T Dequeue()
    {
        throw new NotImplementedException();
    }

    public void Enqueue(T item)
    {
        throw new NotImplementedException();
    }

    public T Peek()
    {
        throw new NotImplementedException();
    }
}
