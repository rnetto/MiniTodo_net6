using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapGet("/v1/afazer", (AppDbContext _context) =>
{
    return Results.Ok(_context.Afazers.AsNoTracking().ToList() ?? new List<Afazer>());
});

app.MapGet("/v1/afazer/{id=int}", async (AppDbContext _context, int id) =>
{
    var afazer = await _context.Afazers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return afazer != null ?  Results.Ok() : Results.NoContent();
});

app.MapPost("/v1/afazer", async (AppDbContext _context, [FromBody] CriarAfazer model) =>
{
    var afazer = new Afazer { Titulo = model.Titulo };
    await _context.AddAsync(afazer);
    await _context.SaveChangesAsync();

    return Results.Created($"/v1/afazer/{afazer.Id}", afazer);
});

app.MapPut("/v1/afazer/{id=int}", async (AppDbContext _context, [FromBody] EditarAfazer model, int id) =>
{
    var afazer = await _context.Afazers.FirstOrDefaultAsync(x => x.Id == id);
    if (afazer is null)
        return Results.NoContent();

    afazer.Titulo = model.Titulo;
    afazer.Realizado = model.Realizado ?? false;
    await _context.SaveChangesAsync();

    return Results.Ok(afazer);
});

app.MapDelete("/v1/afazer/{id=int}", async (AppDbContext _context, int id) =>
{
    var afazer = await _context.Afazers.FirstOrDefaultAsync(x => x.Id == id);
    if (afazer is null)
        return Results.NoContent();

    _context.Remove(afazer);
    await _context.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
