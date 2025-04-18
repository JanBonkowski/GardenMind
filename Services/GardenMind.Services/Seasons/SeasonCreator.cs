using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;
using GardenMind.Persistence;
using GardenMind.Services.Seasons.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Services.Seasons;

public class SeasonCreator
{
    private readonly GardenDbContext _ctx;

    public SeasonCreator(GardenDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<int> Create(CreateSeasonRequest request, CancellationToken cancellationToken = default)
    {
        var anySeasonsPending = await _ctx.Seasons.AnyAsync(x => !x.TerminatedAt.HasValue);
        if (anySeasonsPending)
        {
            throw new SeasonPendingException();
        }

        var newSeason = Season.Create();
        if (request.StartImmediately)
        {
            newSeason.Start();
        }

        _ctx.Seasons.Add(newSeason);
        await _ctx.SaveChangesAsync(cancellationToken);

        return newSeason.Id;
    }
}