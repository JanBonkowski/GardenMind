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
        private readonly SeasonStarter _seasonStarter;

        public SeasonsController(
            ILogger<SeasonsController> logger,
            SeasonCreator seasonCreator,
            SeasonStarter seasonStarter)
        {
            _logger = logger;
            _seasonCreator = seasonCreator;
            _seasonStarter = seasonStarter;
        }

        [HttpPost]
        public async Task<CreatedResult> CreateSeason([FromBody]CreateSeasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to create a new season");
            var newSeasonId = await _seasonCreator.Create(request, cancellationToken);

            return Created($"api/seasons/{newSeasonId}", newSeasonId);
        }

        [HttpPut("started")]
        public async Task StartSeason([FromBody] StartSeasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to start a new season");
            await _seasonStarter.StartSeason(request, cancellationToken);
        }
    }
}
