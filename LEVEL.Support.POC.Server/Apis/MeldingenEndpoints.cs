using LEVEL.Support.POC.Server.Data;
using LEVEL.Support.POC.Server.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LEVEL.Support.POC.Server.Apis;

public static class MeldingenEndpoints
{
    public static RouteGroupBuilder MapMeldingen(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAll);
        group.MapGet("/afgehandeld", GetCompleted);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);

        return group;
    }

    private static async Task<IResult> GetAll(DataContext db)
    {
        var items = await db.Meldingen.ToListAsync();
        return Results.Ok(items);
    }

    private static async Task<IResult> GetCompleted(DataContext db)
    {
        var items = await db.Meldingen
            .Where(m => m.IsAfgehandeld)
            .ToListAsync();

        return Results.Ok(items);
    }

    private static async Task<IResult> GetById(int id, DataContext db)
    {
        var melding = await db.Meldingen
            .Include(m => m.Oplossingen.OrderByDescending(o => o.AangemaaktOp))
            .Include(m => m.GekoppeldeMeldingen)
                .ThenInclude(g => g.Gekoppeld)
            .FirstOrDefaultAsync(m => m.Id == id);

        return melding is not null
            ? Results.Ok(melding)
            : Results.NotFound();
    }

    private static async Task<IResult> Create(Melding melding, DataContext db)
    {
        db.Meldingen.Add(melding);
        await db.SaveChangesAsync();

        return Results.Created($"/api/meldingen/{melding.Id}", melding);
    }

    private static async Task<IResult> Update(int id, Melding input, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(id);

        if (melding is null)
            return Results.NotFound();

        melding.Titel = input.Titel;
        melding.Beschrijving = input.Beschrijving;
        melding.Samenvatting = input.Samenvatting;
        melding.Applicatie = input.Applicatie;
        melding.Categorie = input.Categorie;
        melding.Prioriteit = input.Prioriteit;
        melding.IsAfgehandeld = input.IsAfgehandeld;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> Delete(int id, DataContext db)
    {
        var melding = await db.Meldingen.FindAsync(id);

        if (melding is null)
            return Results.NotFound();

        db.Meldingen.Remove(melding);
        await db.SaveChangesAsync();

        return Results.NoContent();
    }
}