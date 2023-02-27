using Vatify;

var vatNrs = new[] {
    "BE0425234340",          // APOTEEK EUROPARK
    "BE425234340",           // APOTEEK EUROPARK
    "BE425234344",           // invalid
    "CHE-116.281.710 MWST",  // Nestlé S.A. 
    "CHE-221.479.187 MWST",  // Zalando SE, Berlin/D
    "CHE-109.322.551 MWST",  // Test example
    "LU28960386",
    "NL810433930B01",
    "NL820471616B01",
    "NL855392769B01",
    "NL810422852B01",
    "NL004446938B01",
    "NL854593184B01",
    "NL823651071B01",
    "NL803644498B01",
    "NL007051104B01",
    "NL857948283B01",
    "NL808465934B01",
    "NL807562956B01",
    "NL820646660B01",
    "NL821725774B01",
    "FR48533522710",  // Zalando FR
    "FR18529103962",  // Société Zalando Logistics Operations France
    "FR83404833048",
    "FR66780129987",  // Renault
    "FR75642050199",
    "GB118127630",
    "DE260543043",
};

foreach (var item in vatNrs)
{
    var countryCode = item[..2];
    Console.WriteLine($"{item.PadRight(22)} {VatNumberValidator.Validate(countryCode, item)}");
}
