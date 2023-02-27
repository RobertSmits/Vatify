using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class BelgiumVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "BE"
        if (!vatNumber.StartsWith("BE"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

        int sum = int.Parse(digits[..^2]);
        int calculatedCheckDigit = 97 - sum % 97;

        // Check the check digit
        int checkDigit = int.Parse(digits[^2..]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^BE\\d{9,10}$")]
    private static partial Regex VatFormatRegex();
}
