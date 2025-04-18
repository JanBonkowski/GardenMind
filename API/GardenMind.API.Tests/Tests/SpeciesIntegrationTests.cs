using GardenMind.Services.Species.Models;
using GardenMind.Shared.Models;
using NUnit.Framework;

namespace GardenMind.API.Tests.Tests
{
    public class SpeciesIntegrationTests : IntegrationTestBase
    {
        [TestCaseSource(nameof(PageQueryStringGenerator))]
        public async Task ListSpecies(string? queryString)
        {
            // when
            var response = await Client.GetAsync($"api/species{queryString}");

            // then
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task CreateNewSpecies()
        {
            //given
            var request = new CreateSpeciesRequest("Capsicum Annuum");

            // when
            var response = await Client.PostAsJsonAsync($"api/species", request);

            // then
            response.EnsureSuccessStatusCode();
        }

        private static IEnumerable<string?> PageQueryStringGenerator()
        {
            yield return QueryString.Create(
            [
                new KeyValuePair<string, string?>(nameof(PageRequest.PageNumber), "0"),
                new KeyValuePair<string, string?>(nameof(PageRequest.PageSize), "3")
            ]).Value;

            yield return QueryString.Create(new List<KeyValuePair<string, string?>>()).Value;
        }
    }
}