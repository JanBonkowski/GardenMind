using FluentAssertions;
using GardenMind.Domain.Seasons;
using GardenMind.Persistence;
using GardenMind.Services.Seasons;
using GardenMind.Services.Seasons.Models;
using GardenMind.SharedTest;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Seasons;

public class SeasonTerminatorTests
{
    private Mock<GardenDbContext> _ctx;
    private SeasonTerminator _sut;

    private List<Season> _seasons = [];
    [SetUp]
    public void SetUp()
    {
        _seasons = [];
        _ctx = new Mock<GardenDbContext>();
        _ctx.Setup(x => x.Seasons).ReturnsDbSet(_seasons);
        _sut = new SeasonTerminator(_ctx.Object);
    }

    [Test]
    public void Terminates_Season_Successfully()
    {
        // given
        var pendingSeason = SeasonsTestingUtils.NewStartedSeason();
        SeasonsTestingUtils.SetRandomId(pendingSeason);
        _seasons.Add(pendingSeason);
        var request = new TerminateSeasonRequest(pendingSeason.Id);

        // when
        var terminateSeason = async () => await _sut.TerminateSeason(request);

        // then
        terminateSeason.Should().NotThrowAsync();
    }
}
