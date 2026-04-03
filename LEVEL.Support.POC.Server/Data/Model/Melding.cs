namespace LEVEL.Support.POC.Server.Data.Model;

public class Melding
{
    public int Id { get; set; }
    public string Titel { get; set; } = string.Empty;
    public string Beschrijving { get; set; } = string.Empty;
    public string? Applicatie { get; set; }
    public string? Categorie { get; set; }
    public string? Prioriteit { get; set; }
    public bool IsAfgehandeld { get; set; }
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;
}