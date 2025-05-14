using GardenMind.Persistence;
using GardenMind.Services.Seasons.Models;

namespace GardenMind.Services.Seasons;

public class SeasonTerminator : ITerminateSeasons
{
    private GardenDbContext _ctx;

    public SeasonTerminator(GardenDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task TerminateSeason(TerminateSeasonRequest request, CancellationToken cancellationToken = default)
    {
        var season = await _ctx.Seasons.FindAsync(request.Id, cancellationToken);
        if (season == null)
        {
            return;
        }

        season.Terminate();
        await _ctx.SaveChangesAsync();
    }
}
