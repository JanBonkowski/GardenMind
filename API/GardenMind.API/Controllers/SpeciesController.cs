using GardenMind.Services.Species;
using GardenMind.Services.Species.Models;
using GardenMind.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenMind.API.Controllers
{
    [ApiController]
    [Route("api/species")]
    public class SpeciesController : ControllerBase
    {
        private readonly ILogger<SpeciesController> _logger;
        private readonly QuerySpecies _querySpecies;

        public SpeciesController(ILogger<SpeciesController> logger, QuerySpecies querySpecies)
        {
            _logger = logger;
            _querySpecies = querySpecies;
        }

        [HttpGet]
        public async Task<Page<SpeciesListItem>> GetSpeciesList(PageRequest page, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve list of species in application");
            return await _querySpecies.GetAll(page, cancellationToken);
        }
    }
}
