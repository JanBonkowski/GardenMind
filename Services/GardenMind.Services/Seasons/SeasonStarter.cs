using GardenMind.Domain.Exceptions;
using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;
using GardenMind.Persistence;
using GardenMind.Services.Seasons.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Services.Seasons
{
    public class SeasonStarter
    {
        private readonly GardenDbContext _ctx;

        public SeasonStarter(GardenDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task StartSeason(StartSeasonRequest request, CancellationToken cancellationToken = default)
        {
            var anySeasonPending = await _ctx.Seasons.AnyAsync(x => x.StartedAt.HasValue && !x.TerminatedAt.HasValue, cancellationToken);
            if (anySeasonPending)
            {
                throw new SeasonPendingException();
            }

            var season = await _ctx.Seasons.FindAsync(request.Id, cancellationToken);
            if (season is null)
            {
                throw new EntityNotFoundException(nameof(Season));
            }

            season.Start();
            await _ctx.SaveChangesAsync();
        }
    }
}