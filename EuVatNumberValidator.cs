using Vatify.Validators;

namespace Vatify;

#if I_HAVE_MUCH_TIME

// European Union
public class EuVatNumberValidator : IVatNumberValidator
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
            case "AT":
                validator = new AustriaVatNumberValidator();
                break;
            case "BE":
                validator = new BelgiumVatNumberValidator();
                break;
            case "BG":
                validator = new BulgariaVatNumberValidator();
                break;
            case "CY":
                validator = new CyprusVatNumberValidator();
                break;
            case "CZ":
                validator = new CzechRepublicVatNumberValidator();
                break;
            case "DE":
                validator = new GermanyVatNumberValidator();
                break;
            case "DK":
                validator = new DenmarkVatNumberValidator();
                break;
            case "EE":
                validator = new EstoniaVatNumberValidator();
                break;
            case "EL":
            case "GR":
                validator = new GreeceVatNumberValidator();
                break;
            case "ES":
                validator = new SpainVatNumberValidator();
                break;
            case "FI":
                validator = new FinlandVatNumberValidator();
                break;
            case "FR":
                validator = new FranceVatNumberValidator();
                break;
            case "GB":
            case "UK":
                validator = new UnitedKingdomVatNumberValidator();
                break;
            case "HR":
                validator = new CroatiaVatNumberValidator();
                break;
            case "HU":
                validator = new HungaryVatNumberValidator();
                break;
            case "IE":
                validator = new IrelandVatNumberValidator();
                break;
            case "IT":
                validator = new ItalyVatNumberValidator();
                break;
            case "LT":
                validator = new LithuaniaVatNumberValidator();
                break;
            case "LU":
                validator = new LuxembourgVatNumberValidator();
                break;
            case "LV":
                validator = new LatviaVatNumberValidator();
                break;
            case "MT":
                validator = new MaltaVatNumberValidator();
                break;
            case "NL":
                validator = new NetherlandsVatNumberValidator();
                break;
            case "PL":
                validator = new PolandVatNumberValidator();
                break;
            case "PT":
                validator = new PortugalVatNumberValidator();
                break;
            case "RO":
                validator = new RomaniaVatNumberValidator();
                break;
            case "SE":
                validator = new SwedenVatNumberValidator();
                break;
            case "SI":
                validator = new SloveniaVatNumberValidator();
                break;
            case "SK":
                validator = new SlovakiaVatNumberValidator();
                break;
            default:
                return VatNumberValidationResult.InvalidCountryCode();
        }

        return validator.Validate(vatNumber);
    }
}

#endif
