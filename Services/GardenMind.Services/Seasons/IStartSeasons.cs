using GardenMind.Services.Seasons.Models;

namespace GardenMind.Services.Seasons;

public interface IStartSeasons
{
    Task StartSeason(StartSeasonRequest request, CancellationToken cancellationToken = default);
}
