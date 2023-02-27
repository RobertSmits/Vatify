using Vatify.Validators;

namespace Vatify;

public static class VatNumberUtils
{
    private static readonly Dictionary<string, IVatNumberValidator> _validators = new Dictionary<string, IVatNumberValidator>()
    {
        //{"AT", new AustriaVatNumberValidator() },
        {"BE", new BelgiumVatNumberValidator() },
        // {"BG", new BulgariaVatNumberValidator() },
        // {"CY", new CyprusVatNumberValidator() },
        // {"CZ", new CzechRepublicVatNumberValidator() },
        {"DE", new GermanyVatNumberValidator() },
        // {"DK", new DenmarkVatNumberValidator() },
        // {"EE", new EstoniaVatNumberValidator() },
        // {"EL", new GreeceVatNumberValidator() },
        // {"ES", new SpainVatNumberValidator() },
        {"FI", new FinlandVatNumberValidator() },
        {"FR", new FranceVatNumberValidator() },
        // {"GB", new UnitedKingdomVatNumberValidator() },
        // {"HR", new CroatiaVatNumberValidator() },
        // {"HU", new HungaryVatNumberValidator() },
        // {"IE", new IrelandVatNumberValidator() },
        //{"IT", new ItalyVatNumberValidator() },
        // {"LT", new LithuaniaVatNumberValidator() },
        {"LU", new LuxembourgVatNumberValidator() },
        // {"LV", new LatviaVatNumberValidator() },
        // {"MT", new MaltaVatNumberValidator() },
        {"NL", new NetherlandsVatNumberValidator() },
        {"NO", new NorwayVatNumberValidator() },
        // {"PL", new PolandVatNumberValidator() },
        // {"PT", new PortugalVatNumberValidator() },
        // {"RO", new RomaniaVatNumberValidator() },
        {"SE", new SwedenVatNumberValidator() },
        // {"SI", new SloveniaVatNumberValidator() },
        // {"SK", new SlovakiaVatNumberValidator() },
        {"CH", new SwitzerlandVatNumberValidator() },
        // {"LI", new LiechtensteinVatNumberValidator() },
        // {"IS", new IcelandVatNumberValidator() },
        // {"MC", new MonacoVatNumberValidator() },
        // {"RU", new RussiaVatNumberValidator() },
        // {"RS", new SerbiaVatNumberValidator() },
        // {"TR", new TurkeyVatNumberValidator() },
        // {"UA", new UkraineVatNumberValidator() },
        // {"AU", new AustraliaVatNumberValidator() },
        // {"NZ", new NewZealandVatNumberValidator() },
        // {"AR", new ArgentinaVatNumberValidator() },
        // {"BR", new BrazilVatNumberValidator() },
        // {"CA", new CanadaVatNumberValidator() },
        // {"CL", new ChileVatNumberValidator() },
        // {"CO", new ColombiaVatNumberValidator() },
        // {"EC", new EcuadorVatNumberValidator() },
        // {"MX", new MexicoVatNumberValidator() },
        // {"PE", new PeruVatNumberValidator() },
        // {"US", new UnitedStatesVatNumberValidator() }
    };

    public static bool IsValidCountryCode(string countryCode)
    {
        return _validators.ContainsKey(countryCode.ToUpper());
    }

    public static IVatNumberValidator? GetValidator(string countryCode)
    {
        if (_validators.TryGetValue(countryCode.ToUpper(), out IVatNumberValidator? validator))
        {
            return validator;
        }

        return null;
    }

    public static bool IsValidVatNumber(string countryCode, string vatNumber)
    {
        VatNumberValidationResult result = VatNumberValidator.Validate(countryCode, vatNumber);

        return result.IsValid;
    }
}