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
        private readonly ICreateSeasons _seasonCreator;
        private readonly IStartSeasons _seasonStarter;

        public SeasonsController(
            ILogger<SeasonsController> logger,
            ICreateSeasons seasonCreator,
            IStartSeasons seasonStarter)
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
