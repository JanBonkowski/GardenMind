using System.Net.Http.Headers;
using System.Threading.Tasks;
using GardenMind.Shared.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace GardenMind.API.Tests.Tests
{
    public class SpeciesIntegrationTests : IntegrationTestBase
    {
        [TestCaseSource(nameof(PageQueryStringGenerator))]
        public async Task ListSpecies(string? queryString)
        {
            //given
            var client = Client;
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

            // when
            var response = await client.GetAsync($"api/species{queryString}");

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
