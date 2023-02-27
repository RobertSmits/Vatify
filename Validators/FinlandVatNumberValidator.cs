using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class FinlandVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "FI"
        if (!vatNumber.StartsWith("FI"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

        int[] weight = { 7, 9, 10, 5, 8, 4, 2 };
        int sum = 0;

        for (int i = 0; i < 7; i++)
        {
            sum += (int)char.GetNumericValue(digits[i]) * weight[i];
        }

        int calculatedCheckDigit = 11 - sum % 11;
        if (calculatedCheckDigit > 9)
        {
            calculatedCheckDigit = 0;
        }

        // Check the check digit
        int checkDigit = (int)char.GetNumericValue(digits[7]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^FI\\d{8}$")]
    private static partial Regex VatFormatRegex();
}