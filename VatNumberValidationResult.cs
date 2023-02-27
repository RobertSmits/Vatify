namespace Vatify;

public class VatNumberValidationResult
{
    public bool IsValid { get; private set; }
    public string ErrorMessage { get; private set; }

    private VatNumberValidationResult(bool isValid, string errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static VatNumberValidationResult Invalid(string errorMessage)
    {
        return new VatNumberValidationResult(false, errorMessage);
    }

    public static VatNumberValidationResult Valid()
    {
        return new VatNumberValidationResult(true, "");
    }

    public static VatNumberValidationResult InvalidEmptyOrNull()
    {
        return Invalid("VAT number is empty or null.");
    }

    public static VatNumberValidationResult InvalidCountryCode()
    {
        return Invalid("Invalid country code.");
    }

    public static VatNumberValidationResult InvalidFormat()
    {
        return Invalid("Invalid VAT number format.");
    }

    public static VatNumberValidationResult InvalidCheckDigit()
    {
        return Invalid("Invalid check digit.");
    }

    public override string ToString()
    {
        return IsValid ? "Valid VAT number" : ErrorMessage;
    }
}
