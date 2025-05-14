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
        private readonly ITerminateSeasons _seasonTerminator;

        public SeasonsController(
            ILogger<SeasonsController> logger,
            ICreateSeasons seasonCreator,
            IStartSeasons seasonStarter,
            ITerminateSeasons seasonTerminator)
        {
            _logger = logger;
            _seasonCreator = seasonCreator;
            _seasonStarter = seasonStarter;
            _seasonTerminator = seasonTerminator;
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

        [HttpPut("terminated")]
        public async Task<IResult> TerminateSeason([FromRoute] TerminateSeasonRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to terminate a seson with id {id}", request.Id);
            await _seasonTerminator.TerminateSeason(request, cancellationToken);
            return Results.NoContent();
        }
    }
}
