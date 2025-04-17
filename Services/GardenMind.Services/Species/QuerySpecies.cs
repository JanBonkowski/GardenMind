using GardenMind.Persistence;
using GardenMind.Services.Species.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Services.Species;

public class QuerySpecies
{
    private readonly GardenDbContext _ctx;

    public QuerySpecies(GardenDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<SpeciesListResponse> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _ctx.Species.ToListAsync(cancellationToken);
        var mappedResults = result.Select(SpeciesListItem.From);

        return new SpeciesListResponse(mappedResults);
    }
}