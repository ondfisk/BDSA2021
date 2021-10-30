# Notes

```csharp
Parallel.For(0, 999, i => {
    Console.WriteLine(i);
});

Parallel.ForEach(numbers, i => {
    Console.WriteLine(i);
});

Parallel.Invoke(SuperLongRunningThingy1,
    SuperLongRunningThingy2,
    SuperLongRunningThingy3,
    SuperLongRunningThingy4);
```
