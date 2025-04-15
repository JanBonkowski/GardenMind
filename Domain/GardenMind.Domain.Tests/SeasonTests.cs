using FluentAssertions;
using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;

namespace GardenMind.Domain.Tests;

public class SeasonTests
{
    [Test]
    public void Creating_New_Season_Succeeds()
    {
        // when
        Action createSeason = () => Season.Create();

        // then
        Assert.DoesNotThrow(() => createSeason());
    }

    [Test]
    public void New_Season_Has_Created_Date_And_Is_Planned()
    {
        // given
        var season = Season.Create();

        // then
        season.Status.Should().Be(SeasonStatus.Planned);
        season.CreatedAt.Should().NotBe(DateTime.MinValue);
        season.StartedAt.Should().BeNull();
        season.TerminatedAt.Should().BeNull();
    }

    [Test]
    public void Starting_Season_Sets_Status_To_Started()
    {
        // given
        var season = Season.Create();

        // when
        season.Start();

        // then
        season.Started().Should().BeTrue();
        season.Status.Should().Be(SeasonStatus.Started);
        season.StartedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
        season.TerminatedAt.Should().BeNull();
    }

    [Test]
    public void Starting_Season_Should_Throw_For_Already_Started_Season()
    {
        // given
        var season = Season.Create();
        season.Start();

        // when
        Action starting = () => season.Start();

        // then
        starting.Should().Throw<SeasonAlreadyStartedException>();
    }

    [Test]
    public void Starting_Season_Should_Throw_For_Already_Terminated_Season()
    {
        // given
        var season = Season.Create();
        season.Start();
        season.Terminate();

        // when
        Action starting = () => season.Start();

        // then
        starting.Should().Throw<SeasonAlreadyTerminatedException>();
    }

    [Test]
    public void Terminating_New_Season_Should_Throw()
    {
        // given
        var season = Season.Create();

        // when
        Action termination = () => season.Terminate();

        // then
        termination.Should().Throw<SeasonNotStartedException>();
    }

    [Test]
    public void Terminating_Terminated_Season_Should_Throw()
    {
        // given
        var season = Season.Create();
        season.Start();
        season.Terminate();

        // when
        Action termination = () => season.Terminate();

        // then
        termination.Should().Throw<SeasonAlreadyTerminatedException>();
    }

    [Test]
    public void Terminating_Started_Season_Should_Succeed()
    {
        // given
        var season = Season.Create();
        season.Start();

        // when
        season.Terminate();

        // then
        season.Terminated().Should().BeTrue();
        season.Status.Should().Be(SeasonStatus.Terminated);
        season.StartedAt.Should().NotBeNull();
        season.TerminatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(10));
    }
}
