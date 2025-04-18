using GardenMind.Services.Seasons.Models;
using GardenMind.Services.Species.Models;
using GardenMind.Shared.Models;
using NUnit.Framework;

namespace GardenMind.API.Tests.Tests
{
    public class SeasonsIntegrationTests : IntegrationTestBase
    {
        [Test]
        public async Task CreateNewSeason()
        {
            //given
            var request = new CreateSeasonRequest(false);

            // when
            var response = await Client.PostAsJsonAsync($"api/seasons", request);

            // then
            response.EnsureSuccessStatusCode();
        }
    }
}