using System;
using Xunit;

namespace Generics.Tests
{
    public class PostalCodeValidatorTests
    {
        [Theory]
        [InlineData("2345")]
        [InlineData("1234")]
        [InlineData("0000")]
        public void IsValid_given_valid_code_returns_true(string code)
        {
            var val = new PostalCodeValidator();

            var actual = val.IsValid(code);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("stuff 12325 more")]
        public void IsValid_given_invalid_code_returns_true(string code)
        {
            var val = new PostalCodeValidator();

            var actual = val.IsValid(code);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("2000 Frederiksberg")]
        [InlineData("8000 Aarhus C")]
        public void TryParse_given_postal_code_and_locality_returns_true(string postalCodeAndLocality)
        {
            var val = new PostalCodeValidator();

            var actual = val.TryParse(postalCodeAndLocality, out var _, out var _);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("2000 Frederiksberg", "2000")]
        [InlineData("8000 Aarhus C", "8000")]
        public void TryParse_given_postal_code_and_locality_outs_postal_code(string postalCodeAndLocality, string expectedPostalCode)
        {
            var val = new PostalCodeValidator();

            val.TryParse(postalCodeAndLocality, out var actualPostalCode, out var _);

            Assert.Equal(expectedPostalCode, actualPostalCode);
        }

        [Theory]
        [InlineData("2000 Frederiksberg", "Frederiksberg")]
        [InlineData("8000 Aarhus C", "Aarhus C")]
        public void TryParse_given_postal_code_and_locality_outs_locality(string postalCodeAndLocality, string expectedLocality)
        {
            var val = new PostalCodeValidator();

            val.TryParse(postalCodeAndLocality, out var _, out var actualLocality);

            Assert.Equal(expectedLocality, actualLocality);
        }
    }
}
