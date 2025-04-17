using FluentAssertions;
using GardenMind.Persistence;
using GardenMind.Services.Species;
using GardenMind.SharedTest;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Species;

public class QuerySpeciesTests
{
    private Mock<GardenDbContext> _ctx;
    private QuerySpecies _sut;
    
    private static readonly ISet<Domain.Species> SpeciesForTest = SpeciesTestingUtils.MultipleSpecies(10);

    [SetUp]
    public void Setup()
    {
        _ctx = new Mock<GardenDbContext>();
        _ctx.Setup(x => x.Species).ReturnsDbSet(SpeciesForTest);

        _sut = new QuerySpecies(_ctx.Object);
    }

    [Test]
    public async Task Returns_All_Species()
    {
        // when
        var result = await _sut.GetAll();

        // then
        result.Items.Any().Should().BeTrue();
        result.Items.Should().HaveCount(SpeciesForTest.Count);
        var expectedNames = SpeciesForTest.Select(x => x.Name);
        var actualNames = result.Items.Select(x => x.Name);
        actualNames.Should().Contain(expectedNames);
    }
}
