using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class SwedenVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "SE"
        if (!vatNumber.StartsWith("SE"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

        int r = 0;
        for (int i = 0; i < 9; i += 2)
        {
            int value = (int)char.GetNumericValue(digits[i]);
            r += (value / 5) + (value * 2) % 10;
        }

        int s = 0;
        for (int i = 1; i < 9; i += 2)
        {
            s += (int)char.GetNumericValue(digits[i]);
        }

        int calculatedCheckDigit = (10 - (r + s) % 10) % 10;

        // Check the check digit
        int checkDigit = (int)char.GetNumericValue(digits[9]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^SE\\d{10}01$")]
    private static partial Regex VatFormatRegex();
}