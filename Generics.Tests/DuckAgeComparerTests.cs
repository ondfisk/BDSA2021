using Xunit;

namespace Generics.Tests
{
    public class DuckAgeComparerTests
    {
        // Compare should return:
        // a signed integer that indicates the relative values of x and y:
        // - If less than 0, x is less than y.
        // - If 0, x equals y.
        // - If greater than 0, x is greater than y.

        [Fact]
        public void Compare_given_x_less_than_y_returns_minus_1()
        {
            var x = new Duck { Name = "Huey Duck", Age = 10 };
            var y = new Duck { Name = "Scrooge McDuck", Age = 60 };

            var comparer = new DuckAgeComparer();
            var c = comparer.Compare(x, y);

            Assert.Equal(-1, c);
        }

        [Fact]
        public void Compare_given_x_more_than_y_returns_1()
        {
            var x = new Duck { Name = "Magica De Spell", Age = 302 };
            var y = new Duck { Name = "Donald Duck", Age = 32 };

            var comparer = new DuckAgeComparer();
            var c = comparer.Compare(x, y);

            Assert.Equal(1, c);
        }

        [Fact]
        public void Compare_given_x_equals_y_returns_0()
        {
            var x = new Duck { Name = "Huey Duck", Age = 10 };
            var y = new Duck { Name = "Dewey Duck", Age = 10 };

            var comparer = new DuckAgeComparer();
            var c = comparer.Compare(x, y);

            Assert.Equal(0, c);
        }
    }
}
