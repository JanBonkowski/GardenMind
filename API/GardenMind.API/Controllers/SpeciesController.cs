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
        private readonly IQuerySpecies _querySpecies;
        private readonly ICreateSpecies _speciesCreator;

        public SpeciesController(
            ILogger<SpeciesController> logger,
            IQuerySpecies querySpecies,
            ICreateSpecies speciesCreator)
        {
            _logger = logger;
            _querySpecies = querySpecies;
            _speciesCreator = speciesCreator;
        }

        [HttpGet]
        public async Task<Page<SpeciesListItem>> GetSpeciesList([FromQuery]PageRequest page, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve list of species in application");
            return await _querySpecies.GetAll(page, cancellationToken);
        }

        [HttpPost]
        public async Task<CreatedResult> CreateSpecies([FromBody]CreateSpeciesRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create a new species");
            var newSpeciesId = await _speciesCreator.Create(request, cancellationToken);

            return Created("/", newSpeciesId);
        }
    }
}
