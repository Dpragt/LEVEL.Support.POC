using LEVEL.Support.POC.Server.Data.Model;

namespace LEVEL.Support.POC.Server.Data;

public static class DataSeeder
{
    public static void Seed(DataContext context)
    {
        if (context.Meldingen.Any())
            return;

        context.Meldingen.AddRange(
            new Melding
            {
                Titel = "Taxatieverslag wordt niet gegenereerd",
                Beschrijving = "Bij het aanmaken van een nieuw taxatieverslag voor woningen krijgt de gebruiker een timeout error. Dit speelt sinds de laatste update van de taxatiemodule.",
                Applicatie = "Taxatie",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-12)
            },
            new Melding
            {
                Titel = "Herbeoordeling waardepeildatum 2025 loopt vast",
                Beschrijving = "Het bulk-herbeoordelingsproces voor waardepeildatum 1 januari 2025 stopt halverwege zonder foutmelding. Betreft circa 3.000 objecten.",
                Applicatie = "Taxatie",
                Categorie = "Bug",
                Prioriteit = "Kritiek",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-3)
            },
            new Melding
            {
                Titel = "Objectkenmerken niet zichtbaar na import",
                Beschrijving = "Na het importeren van BAG-mutaties worden de gewijzigde objectkenmerken (oppervlakte, bouwjaar) niet correct weergegeven in het objectscherm.",
                Applicatie = "Object",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-8)
            },
            new Melding
            {
                Titel = "Splitsen van object resulteert in dubbele records",
                Beschrijving = "Wanneer een pand wordt gesplitst in meerdere verblijfsobjecten worden er dubbele objectrecords aangemaakt in de database.",
                Applicatie = "Object",
                Categorie = "Bug",
                Prioriteit = "Normaal",
                IsAfgehandeld = true,
                AangemaaktOp = DateTime.UtcNow.AddDays(-20)
            },
            new Melding
            {
                Titel = "Zoekfunctie op BSN werkt niet voor nieuwe subjecten",
                Beschrijving = "Subjecten die na 1 maart 2025 zijn aangemaakt kunnen niet gevonden worden via de BSN-zoekfunctie. Zoeken op naam werkt wel.",
                Applicatie = "Subject",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-5)
            },
            new Melding
            {
                Titel = "Koppeling met BRP voor adreswijzigingen",
                Beschrijving = "Verzoek om automatische verwerking van adreswijzigingen vanuit de BRP zodat subjectgegevens altijd actueel blijven.",
                Applicatie = "Subject",
                Categorie = "Feature",
                Prioriteit = "Normaal",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-15)
            },
            new Melding
            {
                Titel = "Kadastrale mutaties worden niet correct verwerkt",
                Beschrijving = "De automatische verwerking van kadastrale mutaties vanuit het Kadaster mislukt bij percelen met appartementsrechten. Handmatige verwerking is nodig.",
                Applicatie = "Kadastraal",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-2)
            },
            new Melding
            {
                Titel = "Overzicht eigendomsverhoudingen uitbreiden",
                Beschrijving = "Graag een overzichtspagina toevoegen die alle eigendomsverhoudingen per perceel toont inclusief historisch verloop.",
                Applicatie = "Kadastraal",
                Categorie = "Feature",
                Prioriteit = "Laag",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-30)
            },
            new Melding
            {
                Titel = "WOZ-beschikking bevat verkeerd waardegebied",
                Beschrijving = "Bij een aantal objecten in wijk Centrum-Noord wordt een verkeerd waardegebied vermeld op de WOZ-beschikking. De onderliggende data is wel correct.",
                Applicatie = "Woz",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new Melding
            {
                Titel = "Export WOZ-waarden naar landelijke voorziening faalt",
                Beschrijving = "De maandelijkse export van WOZ-waarden naar de Landelijke Voorziening WOZ geeft een validatiefout op het veld 'ingangsdatum waarde'.",
                Applicatie = "Woz",
                Categorie = "Bug",
                Prioriteit = "Kritiek",
                IsAfgehandeld = true,
                AangemaaktOp = DateTime.UtcNow.AddDays(-25)
            },
            new Melding
            {
                Titel = "Belastingaanslag OZB eigenaar niet correct berekend",
                Beschrijving = "De OZB-eigenaarsheffing wordt bij meerdere eigenaren niet proportioneel verdeeld maar volledig aan de eerste eigenaar toegekend.",
                Applicatie = "Belasting",
                Categorie = "Bug",
                Prioriteit = "Kritiek",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-4)
            },
            new Melding
            {
                Titel = "Kwijtscheldingsmodule toevoegen",
                Beschrijving = "Verzoek om een kwijtscheldingsmodule te integreren zodat kwijtscheldingsverzoeken direct vanuit de belastingapplicatie verwerkt kunnen worden.",
                Applicatie = "Belasting",
                Categorie = "Feature",
                Prioriteit = "Normaal",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-18)
            },
            new Melding
            {
                Titel = "Gecombineerde aanslag toont verkeerde bedragen",
                Beschrijving = "Op de gecombineerde aanslag 2025 worden de afvalstoffenheffing en rioolheffing verwisseld weergegeven. De totaalbedragen kloppen wel.",
                Applicatie = "Aanslag",
                Categorie = "Bug",
                Prioriteit = "Hoog",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-6)
            },
            new Melding
            {
                Titel = "Automatische incasso termijnen worden niet aangemaakt",
                Beschrijving = "Voor belastingplichtigen die betalen via automatische incasso worden de termijnen niet correct aangemaakt na het opleggen van de aanslag.",
                Applicatie = "Aanslag",
                Categorie = "Bug",
                Prioriteit = "Kritiek",
                IsAfgehandeld = false,
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new Melding
            {
                Titel = "PDF-export aanslagbiljet bevat geen logo",
                Beschrijving = "Het gemeentelijk logo ontbreekt op de PDF-export van het aanslagbiljet. In de preview wordt het logo wel getoond.",
                Applicatie = "Aanslag",
                Categorie = "Bug",
                Prioriteit = "Laag",
                IsAfgehandeld = true,
                AangemaaktOp = DateTime.UtcNow.AddDays(-40)
            },
            new Melding
            {
                Titel = "Hoe worden niet-woningen getaxeerd?",
                Beschrijving = "Vraag van een nieuwe medewerker over het taxatieproces voor niet-woningen (bedrijfspanden). Graag uitleg over de werkwijze in de applicatie.",
                Applicatie = "Taxatie",
                Categorie = "Vraag",
                Prioriteit = "Laag",
                IsAfgehandeld = true,
                AangemaaktOp = DateTime.UtcNow.AddDays(-10)
            }
        );

        context.SaveChanges();
    }
}
