using GardenMind.Services.Seasons;
using GardenMind.Services.Seasons.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenMind.API.Controllers
{
    [ApiController]
    [Route("api/seasons")]
    public class SeasonsController : ControllerBase
    {
        private readonly ILogger<SeasonsController> _logger;
        private readonly SeasonCreator _seasonCreator;

        public SeasonsController(
            ILogger<SeasonsController> logger,
            SeasonCreator seasonCreator)
        {
            _logger = logger;
            _seasonCreator = seasonCreator;
        }

        [HttpPost]
        public async Task<CreatedResult> CreateSeason([FromBody]CreateSeasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create a new season");
            var newSeasonId = await _seasonCreator.Create(request, cancellationToken);

            return Created("api/seasons/", newSeasonId);
        }
    }
}
