using GardenMind.Persistence;
using GardenMind.Services.Species.Models;

namespace GardenMind.Services.Species;

public class SpeciesCreator : ICreateSpecies
{
    private readonly GardenDbContext _ctx;

    public SpeciesCreator(GardenDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<int> Create(CreateSpeciesRequest request, CancellationToken cancellationToken = default)
    {
        var species = Domain.Species.Create(request.Name);
        _ctx.Species.Add(species);
        await _ctx.SaveChangesAsync(cancellationToken);

        return species.Id;
    }
}