using System;
using Lecture03.Models;

namespace Lecture03
{
    public delegate int BinaryOperation(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
        }

        public static int LocalFunctionFactorial(int n)
        {
            return nthFactorial(n);

            int nthFactorial(int number) => number < 2
                ? 1
                : number * nthFactorial(number - 1);
        }

        public static int LambdaFactorial(int n)
        {
            Func<int, int> nthFactorial = default(Func<int, int>);

            nthFactorial = number => number < 2
                ? 1
                : number * nthFactorial(number - 1);

            return nthFactorial(n);
        }
    }
}
