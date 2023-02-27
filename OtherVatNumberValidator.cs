namespace Vatify;

#if I_HAVE_MUCH_TIME

// Countries outside of European Union
public class OtherVatNumberValidator : IVatNumberValidator
{
    public VatNumberValidationResult Validate(string vatNumber)
    {
        if (vatNumber.Length < 2)
        {
            return VatNumberValidationResult.Invalid("VAT number is too short.");
        }

        string countryCode = vatNumber.Substring(0, 2);
        IVatNumberValidator? validator = null;

        switch (countryCode)
        {
            case "AU":
                validator = new AustraliaVatNumberValidator();
                break;
            case "CA":
                validator = new CanadaVatNumberValidator();
                break;
            case "CN":
                validator = new ChinaVatNumberValidator();
                break;
            case "IN":
                validator = new IndiaVatNumberValidator();
                break;
            case "JP":
                validator = new JapanVatNumberValidator();
                break;
            case "NO":
                validator = new NorwayVatNumberValidator();
                break;
            case "NZ":
                validator = new NewZealandVatNumberValidator();
                break;
            case "RU":
                validator = new RussiaVatNumberValidator();
                break;
            case "US":
                validator = new UnitedStatesVatNumberValidator();
                break;
            default:
                return VatNumberValidationResult.InvalidCountryCode();
        }
        return validator.Validate(vatNumber);
    }
}

#endif
