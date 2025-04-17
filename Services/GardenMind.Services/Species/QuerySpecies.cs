using GardenMind.Persistence;
using GardenMind.Services.Species.Models;
using GardenMind.Shared.Extensions;
using GardenMind.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Services.Species;

public class QuerySpecies
{
    private readonly GardenDbContext _ctx;

    public QuerySpecies(GardenDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Page<SpeciesListItem>> GetAll(PageRequest page, CancellationToken cancellationToken = default)
    {
        var query = _ctx.Species.AsNoTracking();
        var totalNumberOfElements = await query.CountAsync(cancellationToken);

        query = query.Page(page);
        var result = await query.ToListAsync(cancellationToken);

        var mappedResults = result.Select(SpeciesListItem.From);

        return Page<SpeciesListItem>.From(mappedResults, page, totalNumberOfElements);
    }
}