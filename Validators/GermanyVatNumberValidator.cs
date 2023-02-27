using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class GermanyVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "DE"
        if (!vatNumber.StartsWith("DE"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

        int product = 10;
        int calculatedCheckDigit = 0;
        for (int i = 0; i < 8; i++)
        {
            // Extract the next digit and implement peculiar algorithm!
            int sum = (int)Char.GetNumericValue(digits[i]);
            if (sum == 0)
            {
                sum = 10;
            }

            product = (2 * sum) % 11;
        }

        // Establish check digit.
        if (11 - product == 10)
        {
            calculatedCheckDigit = 0;
        }
        else
        {
            calculatedCheckDigit = 11 - product;
        }

        // Check the check digit
        int checkDigit = int.Parse(digits[^1..]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^DE\\d{9}$")]
    private static partial Regex VatFormatRegex();
}
