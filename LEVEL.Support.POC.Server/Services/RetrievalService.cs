using LEVEL.Support.POC.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Services;

public class RetrievalService(DataContext db)
{
    private readonly DataContext _db = db;

    public async Task<List<MeldingCandidate>> GetRecentMeldingenAsync(int count = 20)
    {
        return await _db.Meldingen
            .OrderByDescending(m => m.AangemaaktOp)
            .Take(count)
            .Select(m => new MeldingCandidate
            {
                Id = m.Id,
                Titel = m.Titel,
                Samenvatting = m.Samenvatting
            })
            .ToListAsync();
    }
}

public class MeldingCandidate
{
    public int Id { get; set; }
    public string Titel { get; set; } = string.Empty;
    public string? Samenvatting { get; set; }
}
