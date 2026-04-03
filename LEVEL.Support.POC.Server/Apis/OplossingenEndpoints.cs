using LEVEL.Support.POC.Server.Data;
using LEVEL.Support.POC.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Apis;

public static class OplossingenEndpoints
{
    public static RouteGroupBuilder MapOplossingen(this RouteGroupBuilder group)
    {
        group.MapGet("/{meldingId:int}/oplossingen", GetByMelding);
        group.MapPost("/{meldingId:int}/oplossingen", Create);
        group.MapPut("/{meldingId:int}/oplossingen/{id:int}", Update);
        group.MapDelete("/{meldingId:int}/oplossingen/{id:int}", Delete);

        return group;
    }

    private static async Task<IResult> GetByMelding(int meldingId, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(meldingId);
        if (melding is null)
            return Results.NotFound();

        var items = await db.Oplossingen
            .Where(o => o.MeldingId == meldingId)
            .OrderByDescending(o => o.AangemaaktOp)
            .ToListAsync();

        return Results.Ok(items);
    }

    private static async Task<IResult> Create(int meldingId, Oplossing input, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(meldingId);
        if (melding is null)
            return Results.NotFound();

        input.MeldingId = meldingId;
        db.Oplossingen.Add(input);
        await db.SaveChangesAsync();

        return Results.Created($"/api/meldingen/{meldingId}/oplossingen/{input.Id}", input);
    }

    private static async Task<IResult> Update(int meldingId, int id, Oplossing input, DataContext db)
    {
        var oplossing = await db.Oplossingen
            .FirstOrDefaultAsync(o => o.Id == id && o.MeldingId == meldingId);

        if (oplossing is null)
            return Results.NotFound();

        oplossing.Beschrijving = input.Beschrijving;
        oplossing.Bron = input.Bron;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> Delete(int meldingId, int id, DataContext db)
    {
        var oplossing = await db.Oplossingen
            .FirstOrDefaultAsync(o => o.Id == id && o.MeldingId == meldingId);

        if (oplossing is null)
            return Results.NotFound();

        db.Oplossingen.Remove(oplossing);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}
