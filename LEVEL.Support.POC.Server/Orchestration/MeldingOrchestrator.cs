using LEVEL.Support.POC.Server.Agents;
using LEVEL.Support.POC.Server.Data;
using LEVEL.Support.POC.Server.Data.Model;

namespace LEVEL.Support.POC.Server.Orchestration;

public class MeldingOrchestrator(
    ClassificationAgent classificationAgent,
    DuplicateDetectionAgent duplicateAgent,
    DataContext db,
    ILogger<MeldingOrchestrator> logger)
{
    private readonly ClassificationAgent _classificationAgent = classificationAgent;
    private readonly DuplicateDetectionAgent _duplicateAgent = duplicateAgent;
    private readonly DataContext _db = db;
    private readonly ILogger<MeldingOrchestrator> _logger = logger;

    public async Task<Melding> ProcessAsync(Melding melding)
    {
        _logger.LogInformation("Orchestrator gestart voor melding: {Titel}", melding.Titel);

        // Stap 1: Classificatie
        var classification = await _classificationAgent
            .ClassifyAsync(melding.Titel, melding.Beschrijving);

        melding.Categorie ??= classification.Categorie;
        melding.Prioriteit ??= classification.Prioriteit;
        melding.Applicatie ??= classification.Applicatie;
        melding.Samenvatting ??= classification.Samenvatting;

        // Stap 2: Duplicate detectie
        var duplicates = await _duplicateAgent
            .FindDuplicatesAsync(melding.Titel, melding.Beschrijving);

        // Stap 3: Opslaan melding
        _db.Meldingen.Add(melding);
        await _db.SaveChangesAsync();

        // Stap 4: Relaties leggen
        foreach (var dup in duplicates.Duplicates)
        {
            _db.GekoppeldeMeldingen.Add(new GekoppeldeMelding
            {
                MeldingId = melding.Id,
                GekoppeldeMeldingId = dup.Id,
                Reden = dup.Motivering,
            });
        }

        if (duplicates.Duplicates.Count > 0)
            await _db.SaveChangesAsync();

        _logger.LogInformation(
            "Orchestrator afgerond voor melding {Id}: Categorie={Categorie}, Prioriteit={Prioriteit}, Duplicaten={Aantal}",
            melding.Id, melding.Categorie, melding.Prioriteit, duplicates.Duplicates.Count);

        return melding;
    }
}
