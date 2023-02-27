using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class LuxembourgVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "LU"
        if (!vatNumber.StartsWith("LU"))
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
        int calculatedCheckDigit = sum % 89;

        // Check the check digit
        int checkDigit = int.Parse(digits[^2..]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^LU\\d{8}$")]
    private static partial Regex VatFormatRegex();
}
