namespace MyApp.Core
{
    public class CharacterValidation
    {
        public static ValidationResult? ValidateName(string alterEgo, ValidationContext validationContext)
        {
            var character = (CharacterCreateDto)validationContext.ObjectInstance;

            var names = new[]
            {
                alterEgo,
                character.GivenName,
                character.Surname,
            };

            if (names.All(string.IsNullOrWhiteSpace))
            {
                return new ValidationResult("Alter ego, given name, or surname must be supplied.", new[] { nameof(CharacterDetailsDto.AlterEgo) });
            }

            return ValidationResult.Success;
        }

        public static ValidationResult? ValidatePowers(ISet<string> powers)
        {
            if (powers.Any(p => p.Length > 50))
            {
                return new ValidationResult("A power must be at most 50 characters.");
            }

            return ValidationResult.Success;
        }
    }
}