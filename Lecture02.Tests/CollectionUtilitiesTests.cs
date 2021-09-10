using Xunit;
using System.Collections.Generic;
using static Lecture02.CollectionUtilities;

namespace Lecture02.Tests
{
    public class CollectionUtilitiesTests
    {
        [Fact]
        public void GetEven_given_1_2_3_4_5_returns_2_4()
        {
            // Arrange
            var input = new[] { 1, 2, 3, 4, 5 };

            // Act
            var output = GetEven(input);

            // Assert
            Assert.Equal(new[] { 2, 4 }, output);
        }

        [Fact]
        public void Find_given_number_exists_returns_true()
        {
            // Arrange
            var input = new[] { 1, 2, 3, 4, 5 };

            // Act
            var output = Find(input, 3);

            // Assert
            Assert.True(output);
        }

        [Fact]
        public void Find_given_number_does_not_exist_returns_true()
        {
            // Arrange
            var input = new[] { 1, 2, 3, 4, 5 };

            // Act
            var output = Find(input, 42);

            // Assert
            Assert.False(output);
        }

        [Fact]
        public void Unique_given_3_2_1_2_3_returns_1_2_3()
        {
            // Arrange
            var input = new[] { 3, 2, 1, 2, 3 };

            // Act
            var output = Unique(input);

            // Assert
            Assert.Contains(1, output);
            Assert.Contains(2, output);
            Assert.Contains(3, output);
            Assert.Equal(3, output.Count);
        }

        [Fact]
        public void Reverse_given_1_2_3_4_5_returns_5_4_3_2_1()
        {
            // Arrange
            var input = new[] { 1, 2, 3, 4, 5 };

            // Act
            var output = Reverse(input);

            // Assert
            Assert.Equal(new[] { 5, 4, 3, 2, 1 }, output);
        }
    }
}
