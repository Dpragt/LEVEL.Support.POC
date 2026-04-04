using LEVEL.Support.POC.Server.Agents.Utils;
using LEVEL.Support.POC.Server.Services;
using Microsoft.Extensions.AI;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LEVEL.Support.POC.Server.Agents;

public class DuplicateDetectionAgent(
    IChatClient chat,
    RetrievalService retrieval,
    ILogger<DuplicateDetectionAgent> logger)
{
    private readonly IChatClient _chat = chat;
    private readonly RetrievalService _retrieval = retrieval;
    private readonly ILogger<DuplicateDetectionAgent> _logger = logger;

    public async Task<DuplicateResponse> FindDuplicatesAsync(string titel, string beschrijving)
    {
        var candidates = await _retrieval.GetRecentMeldingenAsync();

        var prompt = $$"""
        Je bent een duplicate detection agent.

        Nieuwe melding:
        Titel: "{{titel}}"
        Beschrijving: "{{beschrijving}}"

        Bestaande meldingen:
        {{JsonSerializer.Serialize(candidates)}}

        Geef een lijst van IDs die waarschijnlijk duplicaten zijn.

        Antwoord ALLEEN in validen JSON:
        {
          "duplicates": [
                {
                    "id": ,
                    "motivering": "<REDEN VAN DUPLICAAT>"
                }
            ]
        }
        """;

        try
        {
            var response = await _chat.GetResponseAsync(prompt);
            var json = response.Text.Trim();
            json = AgentUtils.SanitizeAgentResponseJson(json);

            var result = JsonSerializer.Deserialize<DuplicateResponse>(json);

            if (result is not null)
                return result;

        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Duplicate detectie mislukt");
        }

        return new DuplicateResponse();
    }
}

public class DuplicateResponse
{
    [JsonPropertyName("duplicates")]
    public List<DuplicateMelding> Duplicates { get; set; } = [];
}

public class DuplicateMelding
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("motivering")]
    public string Motivering { get; set; } = string.Empty;
}