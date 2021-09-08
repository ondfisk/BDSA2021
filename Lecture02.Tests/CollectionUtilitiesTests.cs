using Xunit;
using System.Collections.Generic;

namespace Lecture02.Tests
{
    public class CollectionUtilitiesTests
    {
        [Fact]
        public void GetEven_given_1_2_3_4_5_returns_2_4()
        {
            int[] input = { 1, 2, 3, 4, 5 };

            var output = CollectionUtilities.GetEven(input);

            Assert.Equal(new[] { 2, 4 }, output);
        }

        [Fact]
        public void GetEven_given_1_2_42_4_5_returns_2_42()
        {
            int[] input = { 1, 2, 42, 4, 5 };

            var output = CollectionUtilities.GetEven(input);

            Assert.Equal(new[] { 2, 42 }, output);
        }

        [Fact]
        public void Unique_given_1_1_1_2_3_3_returns_1_2_3()
        {
            int[] input = { 1, 1, 2, 3, 3 };

            var output = CollectionUtilities.Unique(input);

            Assert.Equal(new[] { 1, 2, 3 }, output);
        }
    }
}
