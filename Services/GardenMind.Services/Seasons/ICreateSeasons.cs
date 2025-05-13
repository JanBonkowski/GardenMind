using GardenMind.Services.Seasons.Models;

namespace GardenMind.Services.Seasons;

public interface ICreateSeasons
{
    Task<int> Create(CreateSeasonRequest request, CancellationToken cancellationToken = default);
}
