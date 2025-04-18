using FluentAssertions;
using GardenMind.Domain.Seasons;
using GardenMind.Persistence;
using GardenMind.Services.Seasons;
using GardenMind.Services.Seasons.Models;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Seasons
{
    [TestFixture]
    public class SeasonsCreatorTests
    {
        private Mock<GardenDbContext> _ctx;
        private SeasonCreator _sut;

        private List<Season> _seasons = [];

        [SetUp]
        public void Setup()
        {
            _seasons = [];
            _ctx = new Mock<GardenDbContext>();
            _ctx.Setup(x => x.Seasons).ReturnsDbSet(_seasons);
            _ctx.Setup(x => x.Seasons.Add(It.IsAny<Season>())).Callback<Season>((entry) =>
            {
                _seasons.Add(entry);
            });
            _ctx.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() =>
            {
                var insertedSeason = _ctx.Object.Seasons.First();
                var field = typeof(Season).GetProperty(nameof(Season.Id));
                field!.SetValue(insertedSeason, Random.Shared.Next(0, 100));

                return Task.FromResult(1);
            });

            _sut = new SeasonCreator(_ctx.Object);
        }

        [TestCaseSource(nameof(SeasonStatusByRequirement))]
        public async Task Creates_Seasons_Successfully((bool shouldBeStarted, SeasonStatus expectedStatus) expectedData)
        {
            // given
            var request = new CreateSeasonRequest(expectedData.shouldBeStarted);

            // when
            var createdId = await _sut.Create(request);

            // then
            createdId.Should().BeGreaterThan(0);
            var createdSeason = _ctx.Object.Seasons.First();
            createdSeason.Status.Should().Be(expectedData.expectedStatus);
        }

        public static IEnumerable<(bool, SeasonStatus)> SeasonStatusByRequirement()
        {
            yield return (false, SeasonStatus.Planned);
            yield return (true, SeasonStatus.Started);
        }
    }
}