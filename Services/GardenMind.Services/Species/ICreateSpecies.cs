using GardenMind.Services.Species.Models;

namespace GardenMind.Services.Species
{
    public interface ICreateSpecies
    {
        Task<int> Create(CreateSpeciesRequest request, CancellationToken cancellationToken = default);
    }
}