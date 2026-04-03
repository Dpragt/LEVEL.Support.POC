namespace LEVEL.Support.POC.Server.Data.Model;

public class GekoppeldeMelding
{
    public int Id { get; set; }
    public int MeldingId { get; set; }
    public int GekoppeldeMeldingId { get; set; }
    public string? Reden { get; set; }
    public DateTime AangemaaktOp { get; set; } = DateTime.UtcNow;

    public Melding Melding { get; set; } = null!;
    public Melding Gekoppeld { get; set; } = null!;
}
