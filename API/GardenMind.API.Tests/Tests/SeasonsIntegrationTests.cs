using GardenMind.Services.Seasons.Models;
using NUnit.Framework;

namespace GardenMind.API.Tests.Tests
{
    public class SeasonsIntegrationTests : IntegrationTestBase
    {
        [Test]
        public async Task Creates_New_Season()
        {
            //given
            var request = new CreateSeasonRequest(false);

            // when
            var response = await Client.PostAsJsonAsync($"api/seasons", request);

            // then
            response.EnsureSuccessStatusCode();
        }

        private async Task<string> CreateNewSeason()
        {
            var request = new CreateSeasonRequest(false);
            var response = await Client.PostAsJsonAsync($"api/seasons", request);
            response.EnsureSuccessStatusCode();
            var locationHeader = response.Headers.Location!;
            return locationHeader.OriginalString.Split('/').Last();
        }

        [Test]
        public async Task Starts_New_Season()
        {
            //given
            var id = await CreateNewSeason();

            // when
            var response = await Client.PutAsJsonAsync($"api/seasons/started", new StartSeasonRequest(int.Parse(id)));

            // then
            response.EnsureSuccessStatusCode();
        }
    }
}