using LEVEL.Support.POC.Server.Agents.Utils;
using Microsoft.Extensions.AI;
using System.Text.Json;

namespace LEVEL.Support.POC.Server.Agents;

public class ClassificationAgent(
    IChatClient chat,
    ILogger<ClassificationAgent> logger)
{
    private readonly IChatClient _chat = chat;
    private readonly ILogger<ClassificationAgent> _logger = logger;

    public async Task<ClassificationResult> ClassifyAsync(string titel, string beschrijving)
    {
        var prompt = $$"""
            Je bent een classificatie-agent voor een support systeem van een lokale belasting en waarderings applicatie.
            Classificeer de volgende melding op basis van de titel en beschrijving.

            De beschikbare applicaties zijn:
            - Taxatie (taxatieverslagen, waardebepalingen, herbeoordelingen)
            - Object (objectkenmerken, BAG-mutaties, panden, verblijfsobjecten)
            - Subject (belastingplichtigen, BSN, BRP, adresgegevens)
            - Kadastraal (kadastrale mutaties, percelen, eigendomsverhoudingen, appartementsrechten)
            - Woz (WOZ-beschikkingen, WOZ-waarden, waardegebieden, Landelijke Voorziening)
            - Belasting (belastingberekening, OZB, heffingen, kwijtschelding)
            - Aanslag (aanslagbiljetten, gecombineerde aanslagen, incasso, betalingen)

            De beschikbare categorieën zijn:
            - Bug (iets werkt niet correct)
            - Feature (verzoek om nieuwe functionaliteit)
            - Vraag (informatievraag)
            - Overig (past niet in bovenstaande)

            De beschikbare prioriteiten zijn:
            - Laag (cosmetisch of informatief)
            - Normaal (standaard prioriteit)
            - Hoog (belangrijk, heeft impact op werkprocessen)
            - Kritiek (blokkerend, data-integriteit in gevaar, financiële impact)

            Melding:
            Titel: "{{titel}}"
            Beschrijving: "{{beschrijving}}"

            Antwoord uitsluitend met geldige JSON in het volgende formaat, zonder extra tekst:
            {
              "categorie": "Bug|Feature|Vraag|Overig",
              "prioriteit": "Laag|Normaal|Hoog|Kritiek",
              "applicatie": "Taxatie|Object|Subject|Kadastraal|Woz|Belasting|Aanslag",
              "samenvatting": "Korte samenvatting van de melding in één zin."
            }
            """;

        try
        {
            var response = await _chat.GetResponseAsync(prompt);
            var json = response.Text.Trim();
            json = AgentUtils.SanitizeAgentResponseJson(json);

            var result = JsonSerializer.Deserialize<ClassificationResult>(json);

            if (result is not null)
            {
                _logger.LogInformation(
                    "Melding geclassificeerd: Categorie={Categorie}, Prioriteit={Prioriteit}, Applicatie={Applicatie}",
                    result.Categorie, result.Prioriteit, result.Applicatie);
                return result;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI-classificatie mislukt, standaardwaarden worden gebruikt");
        }

        return new ClassificationResult();
    }
}
