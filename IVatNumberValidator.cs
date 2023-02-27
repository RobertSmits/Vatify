namespace Vatify;

public interface IVatNumberValidator
{
    VatNumberValidationResult Validate(string vatNumber);
}