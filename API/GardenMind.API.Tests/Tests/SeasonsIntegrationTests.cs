using GardenMind.Persistence;
using GardenMind.Services.Seasons.Models;
using GardenMind.SharedTest;
using NUnit.Framework;

namespace GardenMind.API.Tests.Tests
{
    public class SeasonsIntegrationTests : IntegrationTestBase
    {
        private GardenDbContext _ctx;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _ctx = Context();
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _ctx.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _ctx.Seasons.RemoveRange(_ctx.Seasons.ToList());
            _ctx.SaveChanges();
        }

        [Test]
        public async Task Creates_New_Season()
        {
            //given
            var request = new CreateSeasonRequest(false);

            // when
            var response = await Client.PostAsJsonAsync("api/seasons", request);

            // then
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Starts_New_Season()
        {
            //given
            var season = SeasonsTestingUtils.NewPlannedSeason();
            _ctx.Seasons.Add(season);
            await _ctx.SaveChangesAsync();

            // when
            var response = await Client.PutAsJsonAsync("api/seasons/started", new StartSeasonRequest(season.Id));

            // then
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Terminates_Season()
        {
            // given
            var season = SeasonsTestingUtils.NewStartedSeason();
            _ctx.Seasons.Add(season);
            await _ctx.SaveChangesAsync();

            // when
            var response = await Client.PutAsJsonAsync("api/seasons/terminated", new TerminateSeasonRequest(season.Id));

            // then
            response.EnsureSuccessStatusCode();
        }
    }
}