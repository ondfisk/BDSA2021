using System;
using System.Collections.Generic;
using Lecture03.Models;
using static System.Console;

namespace Lecture03
{
    public delegate int BinaryOperation(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            var add = new BinaryOperation(
                delegate (int x, int y)
                {
                    return x + y;
                }
            );

            var results = Compute(4, 2, add, Subtract);

            foreach (var result in results)
            {
                WriteLine(result);
            }
        }

        static int Subtract(int x, int y) => x - y;

        static IEnumerable<(int, int, int)> Compute(int x, int y, params BinaryOperation[] operations)
        {
            foreach (var operation in operations)
            {
                yield return (x, y, operation(x, y));
            }
        }

        #region Local Functions
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
        #endregion
    }
}
