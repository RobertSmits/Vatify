using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class FranceVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "FR"
        if (!vatNumber.StartsWith("FR"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        bool isNonCheckMatch = VatFormatRegex().IsMatch(vatNumber);
        bool isToCheckMatch = VatFormatRegexSecondary().IsMatch(vatNumber);
        if (!isNonCheckMatch && !isToCheckMatch)
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        if (isToCheckMatch)
        {
            ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

            int sum = int.Parse(digits[2..]);
            int calculatedCheckDigit = (12 + 3 * (sum % 97)) % 97;

            // Check the check digit
            int checkDigit = int.Parse(digits[..2]);
            if (calculatedCheckDigit != checkDigit)
            {
                return VatNumberValidationResult.InvalidCheckDigit();
            }
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^FR[A-HJ-NP-Z0-9]{2}\\d{9}$")]
    private static partial Regex VatFormatRegex();

    [GeneratedRegex("^FR\\d{11}$")]
    private static partial Regex VatFormatRegexSecondary();
}
