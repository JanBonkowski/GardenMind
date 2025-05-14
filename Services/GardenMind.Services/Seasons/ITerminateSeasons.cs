using GardenMind.Services.Seasons.Models;

namespace GardenMind.Services.Seasons;

public interface ITerminateSeasons
{
    Task TerminateSeason(TerminateSeasonRequest request, CancellationToken cancellationToken = default);
}
