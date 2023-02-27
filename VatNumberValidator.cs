namespace Vatify;

public static class VatNumberValidator
{
    public static VatNumberValidationResult Validate(string countryCode, string vatNumber)
    {
        if (string.IsNullOrEmpty(countryCode))
        {
            return VatNumberValidationResult.Invalid("Country code is required");
        }

        if (string.IsNullOrEmpty(vatNumber))
        {
            return VatNumberValidationResult.Invalid("VAT number is required");
        }

        if (!VatNumberUtils.IsValidCountryCode(countryCode))
        {
            return VatNumberValidationResult.Invalid($"VAT number validation is not supported for {countryCode}");
        }

        IVatNumberValidator? validator = VatNumberUtils.GetValidator(countryCode);

        if (validator == null)
        {
            return VatNumberValidationResult.Invalid($"VAT number validation is not supported for {countryCode}");
        }

        return validator.Validate(vatNumber);
    }
}