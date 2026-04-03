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
                Samenvatting = "Bij het splitsen van panden in verblijfsobjecten ontstaan dubbele records door een ontbrekende unique-constraint op de BAG-identificatie.",
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
                Samenvatting = "WOZ-export naar LV WOZ faalt door ongeldig datumformaat in het veld 'ingangsdatum waarde' na schema-wijziging van de landelijke voorziening.",
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
                Samenvatting = "Het gemeentelogo ontbreekt in de PDF-export van aanslagbiljetten doordat het pad naar het logo-bestand niet correct werd meegegeven aan de PDF-renderer.",
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
                Samenvatting = "Informatievraag over het taxatieproces voor niet-woningen (bedrijfspanden) in de Taxatie-applicatie.",
                Applicatie = "Taxatie",
                Categorie = "Vraag",
                Prioriteit = "Laag",
                IsAfgehandeld = true,
                AangemaaktOp = DateTime.UtcNow.AddDays(-10)
            }
        );

        context.SaveChanges();

        // Oplossingen seeden voor afgehandelde meldingen
        var splitsenObject = context.Meldingen.First(m => m.Titel.Contains("Splitsen van object"));
        var exportWoz = context.Meldingen.First(m => m.Titel.Contains("Export WOZ-waarden"));
        var pdfAanslag = context.Meldingen.First(m => m.Titel.Contains("PDF-export aanslagbiljet"));
        var nietWoningen = context.Meldingen.First(m => m.Titel.Contains("niet-woningen getaxeerd"));

        // Ook oplossingen bij een paar open meldingen (als suggestie)
        var taxatieVerslag = context.Meldingen.First(m => m.Titel.Contains("Taxatieverslag wordt niet gegenereerd"));
        var bsnZoek = context.Meldingen.First(m => m.Titel.Contains("Zoekfunctie op BSN"));
        var kadMutaties = context.Meldingen.First(m => m.Titel.Contains("Kadastrale mutaties"));
        var ozbEigenaar = context.Meldingen.First(m => m.Titel.Contains("OZB eigenaar"));
        var objectKenmerken = context.Meldingen.First(m => m.Titel.Contains("Objectkenmerken niet zichtbaar"));
        var wozBeschikking = context.Meldingen.First(m => m.Titel.Contains("WOZ-beschikking bevat verkeerd"));
        var incassoTermijnen = context.Meldingen.First(m => m.Titel.Contains("incasso termijnen"));
        var gecombineerdeAanslag = context.Meldingen.First(m => m.Titel.Contains("Gecombineerde aanslag"));

        context.Oplossingen.AddRange(
            // Afgehandelde meldingen — definitieve oplossingen
            new Oplossing
            {
                MeldingId = splitsenObject.Id,
                Beschrijving = "Oorzaak gevonden: de stored procedure sp_SplitsObject controleerde niet op bestaande BAG-identificaties. Een UNIQUE constraint toegevoegd op de kolom BagIdentificatie in de tabel Objecten en de stored procedure aangepast om eerst te controleren of het object al bestaat.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-18)
            },
            new Oplossing
            {
                MeldingId = exportWoz.Id,
                Beschrijving = "De Landelijke Voorziening WOZ heeft per 1 januari 2025 het XML-schema gewijzigd. Het veld 'ingangsdatumWaarde' verwacht nu het formaat yyyy-MM-dd in plaats van dd-MM-yyyy. De exportmodule is aangepast in WozExportService.cs om het nieuwe datumformaat te gebruiken.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-23)
            },
            new Oplossing
            {
                MeldingId = exportWoz.Id,
                Beschrijving = "Na de fix is een volledige herexport uitgevoerd voor alle WOZ-waarden met waardepeildatum 2025. Alle 12.847 objecten zijn succesvol geëxporteerd en gevalideerd door de LV WOZ.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-22)
            },
            new Oplossing
            {
                MeldingId = pdfAanslag.Id,
                Beschrijving = "Het pad naar het logo-bestand was relatief geconfigureerd en werkte alleen in de preview-modus. In de PDF-renderer (AanslagPdfGenerator.cs) het pad gewijzigd naar een absoluut pad via de configuratiesleutel 'Gemeente:LogoPad'. Tevens een fallback ingebouwd die een standaard logo toont als het bestand niet gevonden wordt.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-38)
            },
            new Oplossing
            {
                MeldingId = nietWoningen.Id,
                Beschrijving = "Uitleg gegeven aan de medewerker: niet-woningen worden getaxeerd via de methode 'huurwaardekapitalisatie' (HWK). In de Taxatie-applicatie kies je bij een nieuw taxatieverslag het objecttype 'Niet-woning' waarna automatisch het HWK-model wordt geladen. De referentiehuren worden jaarlijks bijgewerkt vanuit het marktonderzoek. Verwezen naar het handboek hoofdstuk 7.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-9)
            },

            // Open meldingen — AI-suggesties en tussenoplossingen
            new Oplossing
            {
                MeldingId = taxatieVerslag.Id,
                Beschrijving = "Mogelijke oorzaak: de timeout treedt op bij de aanroep naar de TaxatieReportService die een synchrone database-call doet op de Modelwaarden-tabel. Suggestie: controleer of de index op de kolom Waardepeildatum nog intact is en overweeg de query te optimaliseren met een gefilterde index.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-11)
            },
            new Oplossing
            {
                MeldingId = bsnZoek.Id,
                Beschrijving = "Vergelijkbare melding gevonden (ID " + splitsenObject.Id + "): bij het splitsen van objecten was ook een indexprobleem de oorzaak. Controleer of de zoekindex op het BSN-veld is bijgewerkt na de laatste migratie van 1 maart 2025. Mogelijk moet de full-text index opnieuw opgebouwd worden.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-4)
            },
            new Oplossing
            {
                MeldingId = kadMutaties.Id,
                Beschrijving = "Het verwerken van appartementsrechten vereist een specifiek kadastraal berichttype (KAD-APT). De huidige parser in KadasterMutatieProcessor.cs herkent alleen KAD-PRC (percelen). Suggestie: voeg parsing toe voor het KAD-APT berichttype en map de appartementsindex naar het ObjectAppartement-veld.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new Oplossing
            {
                MeldingId = ozbEigenaar.Id,
                Beschrijving = "De berekening in BelastingBerekenService.cs pakt de eerste eigenaar uit de lijst via .First() in plaats van proportioneel te verdelen. Suggestie: pas de methode BerekenOzbEigenaar aan om het eigendomspercentage uit de kadastrale registratie te gebruiken voor de verdeling.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-3)
            },
            new Oplossing
            {
                MeldingId = objectKenmerken.Id,
                Beschrijving = "Tijdelijke workaround: na de BAG-import handmatig de cache legen via Beheer > Systeem > Cache vernieuwen. De objectkenmerken zijn dan direct zichtbaar. Structurele oplossing vereist aanpassing van de BAG-importprocedure om automatisch de cache te invalideren.",
                Bron = "Handmatig",
                AangemaaktOp = DateTime.UtcNow.AddDays(-7)
            },
            new Oplossing
            {
                MeldingId = wozBeschikking.Id,
                Beschrijving = "Het waardegebied wordt opgehaald uit de tabel WaardegebiedToewijzing op basis van postcode. Voor Centrum-Noord zijn recent postcodes gewijzigd maar de toewijzingstabel is niet bijgewerkt. Suggestie: voer een update uit op WaardegebiedToewijzing voor de postcodes 1234AB t/m 1234AZ.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new Oplossing
            {
                MeldingId = incassoTermijnen.Id,
                Beschrijving = "In de AanslagVerwerkingService wordt na het opleggen van de aanslag de methode MaakIncassoTermijnen alleen aangeroepen als het betalingstype 'INCASSO' is. Controleer of bij recente aanslagen het betalingstype correct is ingesteld. Mogelijk is een standaardwaarde gewijzigd bij de laatste configuratie-update.",
                Bron = "AI-suggestie",
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            }
        );

        context.SaveChanges();

        // Koppelingen tussen gerelateerde meldingen
        context.GekoppeldeMeldingen.AddRange(
            new GekoppeldeMelding
            {
                MeldingId = objectKenmerken.Id,
                GekoppeldeMeldingId = splitsenObject.Id,
                Reden = "Beide meldingen betreffen data-integriteit in de Object-applicatie na een mutatieverwerking.",
                AangemaaktOp = DateTime.UtcNow.AddDays(-7)
            },
            new GekoppeldeMelding
            {
                MeldingId = wozBeschikking.Id,
                GekoppeldeMeldingId = exportWoz.Id,
                Reden = "Beide meldingen betreffen onjuiste WOZ-gegevens. De exportfout kan gerelateerd zijn aan dezelfde data-inconsistentie.",
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new GekoppeldeMelding
            {
                MeldingId = gecombineerdeAanslag.Id,
                GekoppeldeMeldingId = incassoTermijnen.Id,
                Reden = "Beide meldingen betreffen de aanslagverwerking 2025 en kunnen een gemeenschappelijke oorzaak hebben in de configuratie-update.",
                AangemaaktOp = DateTime.UtcNow.AddDays(-1)
            },
            new GekoppeldeMelding
            {
                MeldingId = ozbEigenaar.Id,
                GekoppeldeMeldingId = kadMutaties.Id,
                Reden = "De OZB-berekening is afhankelijk van correcte kadastrale eigendomsgegevens. De foutieve mutatieverwerking kan de OZB-berekening beïnvloeden.",
                AangemaaktOp = DateTime.UtcNow.AddDays(-2)
            }
        );

        context.SaveChanges();
    }
}
