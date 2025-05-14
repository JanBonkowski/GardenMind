using FluentAssertions;
using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;
using GardenMind.Persistence;
using GardenMind.Services.Seasons;
using GardenMind.Services.Seasons.Models;
using GardenMind.SharedTest;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Seasons
{
    public class SeasonStarterTests
    {
        private Mock<GardenDbContext> _ctx;
        private SeasonStarter _sut;

        private List<Season> _seasons = [];

        [SetUp]
        public void Setup()
        {
            _seasons = [];
            _ctx = new Mock<GardenDbContext>();
            _ctx.Setup(x => x.Seasons).ReturnsDbSet(_seasons);
            _sut = new SeasonStarter(_ctx.Object);
        }

        [Test]
        public async Task Can_Not_Start_Two_Seasons_Simultaneously()
        {
            // given
            var startedSeason = SeasonsTestingUtils.NewStartedSeason();
            SeasonsTestingUtils.SetRandomId(startedSeason);
            var seasonToBeStarted = SeasonsTestingUtils.NewPlannedSeason();
            SeasonsTestingUtils.SetRandomId(seasonToBeStarted);
            _seasons.AddRange([startedSeason, seasonToBeStarted]);
            var request = new StartSeasonRequest(seasonToBeStarted.Id);

            // when
            var attemptToStartSeasons = async () => await _sut.StartSeason(request);

            // then
            await attemptToStartSeasons.Should().ThrowAsync<SeasonPendingException>();
        }

        [Test]
        public async Task Starts_Season_Successfully()
        {
            // given
            var seasonToBeStarted = SeasonsTestingUtils.NewPlannedSeason();
            SeasonsTestingUtils.SetRandomId(seasonToBeStarted);
            _seasons.Add(seasonToBeStarted);
            _ctx.Setup(x => x.Seasons.FindAsync(seasonToBeStarted.Id, It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.FromResult(seasonToBeStarted));
            var request = new StartSeasonRequest(seasonToBeStarted.Id);

            // when
            await _sut.StartSeason(request);

            // then
            seasonToBeStarted.Status.Should().Be(SeasonStatus.Started);
        }
    }
}