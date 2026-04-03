namespace LEVEL.Support.POC.Server.Data.Model;

public class Oplossing
{
    public int Id { get; set; }
    public int MeldingId { get; set; }
    public string Beschrijving { get; set; } = string.Empty;
    public string Bron { get; set; } = "Handmatig";
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;

    public Melding Melding { get; set; } = null!;
}
