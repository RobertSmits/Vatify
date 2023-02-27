using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class SwitzerlandVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "CH"
        if (!vatNumber.StartsWith("CH"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        //if (!Regex.IsMatch(vatNumber, @"^CHE\d{9}(MWST)?$"))
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = string.Concat(vatNumber.AsSpan(4, 3), vatNumber.AsSpan(8, 3), vatNumber.AsSpan(12, 3)).AsSpan();

        int[] weight = { 5, 4, 3, 2, 7, 6, 5, 4 };
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
        var checkDigit = (int)char.GetNumericValue(digits[8]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^CHE-\\d{3}\\.\\d{3}\\.\\d{3} (MWST)?$")]
    // [GeneratedRegex("^CHE-\\d{3}\\.\\d{3}\\.\\d{3}( (MWST|TVA|IVA))?$")]
    private static partial Regex VatFormatRegex();
}
