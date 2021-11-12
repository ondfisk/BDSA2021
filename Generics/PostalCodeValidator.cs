namespace Generics;

public class PostalCodeValidator
{
    public static bool IsValid(string postalCode)
    {
        var pattern = @"^\d{4}$";

        var valid = Regex.IsMatch(postalCode, pattern);

        return valid;
    }

    public static bool TryParse(string postalCodeAndLocality,
        out string postalCode,
        out string locality)
    {
        var pattern = @"(?<postal_code>^\d{4}) (?<locality>.+$)";

        var match = Regex.Match(postalCodeAndLocality, pattern);

        postalCode = string.Empty;
        locality = string.Empty;

        if (match.Success)
        {
            postalCode = match.Groups["postal_code"].Value;
            locality = match.Groups["locality"].Value;
        }

        return match.Success;
    }
}
