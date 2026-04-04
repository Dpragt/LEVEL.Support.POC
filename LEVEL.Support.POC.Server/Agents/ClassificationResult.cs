using System.Text.Json.Serialization;

namespace LEVEL.Support.POC.Server.Agents;

public class ClassificationResult
{
    [JsonPropertyName("categorie")]
    public string Categorie { get; set; } = "Overig";

    [JsonPropertyName("prioriteit")]
    public string Prioriteit { get; set; } = "Normaal";

    [JsonPropertyName("applicatie")]
    public string? Applicatie { get; set; }

    [JsonPropertyName("samenvatting")]
    public string? Samenvatting { get; set; }
}
