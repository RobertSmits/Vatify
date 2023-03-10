using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class NorwayVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "NO"
        if (!vatNumber.StartsWith("NO"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2, 9);

        int[] weight = { 3, 2, 7, 6, 5, 4, 3, 2 };
        int sum = 0;

        for (int i = 0; i < 8; i++)
        {
            sum += (int)char.GetNumericValue(digits[i]) * weight[i];
        }

        int calculatedCheckDigit = 11 - sum % 11;
        if (calculatedCheckDigit == 10)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        if (calculatedCheckDigit == 11)
        {
            calculatedCheckDigit = 0;
        }

        // Check the check digit
        int checkDigit = (int)char.GetNumericValue(digits[8]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^NO\\d{9}(MVA)?$")]
    private static partial Regex VatFormatRegex();
}