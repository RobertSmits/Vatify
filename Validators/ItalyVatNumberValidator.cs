using System.Text.RegularExpressions;

namespace Vatify.Validators;

public partial class ItalyVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (string.IsNullOrWhiteSpace(vatNumber))
        {
            return VatNumberValidationResult.InvalidEmptyOrNull();
        }

        // Check that the first two characters are "IT"
        if (!vatNumber.StartsWith("IT"))
        {
            return VatNumberValidationResult.InvalidCountryCode();
        }

        // Check the format of the VAT number
        if (!VatFormatRegex().IsMatch(vatNumber))
        {
            return VatNumberValidationResult.InvalidFormat();
        }

        ReadOnlySpan<char> digits = vatNumber.AsSpan(2);

        int issuingOffice = int.Parse(digits[^4..^1]);
        if (issuingOffice == 0 || (issuingOffice > 201 && issuingOffice != 888 && issuingOffice != 999))
        {
            return VatNumberValidationResult.Invalid("Invalid VAT Nr.");            
        }

        int[] weight = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        int sum = 0;

        for (int i = 0; i < 10; i++)
        {
            var value = (int)char.GetNumericValue(digits[i]) * weight[i];
            sum += value > 9 ? 1 + value % 10 : sum;
        }

        int calculatedCheckDigit = (10 - sum % 10) % 10;

        // Check the check digit
        int checkDigit = (int)char.GetNumericValue(digits[10]);
        if (calculatedCheckDigit != checkDigit)
        {
            return VatNumberValidationResult.InvalidCheckDigit();
        }

        return VatNumberValidationResult.Valid();
    }

    [GeneratedRegex("^IT\\d{11}$")]
    private static partial Regex VatFormatRegex();
}
