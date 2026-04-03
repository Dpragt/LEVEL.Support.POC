using LEVEL.Support.POC.Server.Data;
using LEVEL.Support.POC.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Apis;

public static class GekoppeldeMeldingenEndpoints
{
    public static RouteGroupBuilder MapGekoppeldeMeldingen(this RouteGroupBuilder group)
    {
        group.MapGet("/{meldingId:int}/koppelingen", GetByMelding);
        group.MapPost("/{meldingId:int}/koppelingen", Create);
        group.MapDelete("/{meldingId:int}/koppelingen/{id:int}", Delete);

        return group;
    }

    private static async Task<IResult> GetByMelding(int meldingId, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(meldingId);
        if (melding is null)
            return Results.NotFound();

        var koppelingen = await db.GekoppeldeMeldingen
            .Where(g => g.MeldingId == meldingId || g.GekoppeldeMeldingId == meldingId)
            .Select(g => new
            {
                g.Id,
                g.MeldingId,
                g.GekoppeldeMeldingId,
                g.Reden,
                g.AangemaaktOp,
                GekoppeldeTitel = g.MeldingId == meldingId
                    ? g.Gekoppeld.Titel
                    : g.Melding.Titel,
                GekoppeldeApplicatie = g.MeldingId == meldingId
                    ? g.Gekoppeld.Applicatie
                    : g.Melding.Applicatie,
                GekoppeldeId = g.MeldingId == meldingId
                    ? g.GekoppeldeMeldingId
                    : g.MeldingId
            })
            .OrderByDescending(g => g.AangemaaktOp)
            .ToListAsync();

        return Results.Ok(koppelingen);
    }

    private static async Task<IResult> Create(int meldingId, GekoppeldeMelding input, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(meldingId);
        var gekoppeld = await db.Meldingen.FindAsync(input.GekoppeldeMeldingId);

        if (melding is null || gekoppeld is null)
            return Results.NotFound();

        if (meldingId == input.GekoppeldeMeldingId)
            return Results.BadRequest("Een melding kan niet aan zichzelf gekoppeld worden.");

        input.MeldingId = meldingId;
        db.GekoppeldeMeldingen.Add(input);
        await db.SaveChangesAsync();

        return Results.Created($"/api/meldingen/{meldingId}/koppelingen/{input.Id}", input);
    }

    private static async Task<IResult> Delete(int meldingId, int id, DataContext db)
    {
        var koppeling = await db.GekoppeldeMeldingen
            .FirstOrDefaultAsync(g => g.Id == id &&
                (g.MeldingId == meldingId || g.GekoppeldeMeldingId == meldingId));

        if (koppeling is null)
            return Results.NotFound();

        db.GekoppeldeMeldingen.Remove(koppeling);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}
