namespace Generics;

public class DuckAgeComparer : IComparer<Duck>
{
    public int Compare(Duck? x, Duck? y)
    {
        if (x?.Age < y?.Age)
        {
            return -1;
        }
        if (x?.Age > y?.Age)
        {
            return 1;
        }

        return 0;
    }

    public static Comparison<Duck> Comparison => new DuckAgeComparer().Compare;
}
