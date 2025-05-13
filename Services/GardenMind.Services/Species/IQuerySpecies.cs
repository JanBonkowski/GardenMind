using GardenMind.Services.Species.Models;
using GardenMind.Shared.Models;

namespace GardenMind.Services.Species
{
    public interface IQuerySpecies
    {
        Task<Page<SpeciesListItem>> GetAll(PageRequest page, CancellationToken cancellationToken = default);
    }
}