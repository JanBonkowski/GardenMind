using FluentAssertions;
using GardenMind.Persistence;
using GardenMind.Services.Species;
using GardenMind.Shared.Extensions;
using GardenMind.Shared.Models;
using GardenMind.SharedTest;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Species;

public class QuerySpeciesTests
{
    private Mock<GardenDbContext> _ctx;
    private QuerySpecies _sut;
    
    private static readonly ISet<Domain.Species> SpeciesForTest = SpeciesTestingUtils.MultipleSpecies(123);

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
        // given
        var page = new PageRequest(0, 10);
        var expectedNumberOfItems = SpeciesForTest.Count;

        // when
        var result = await _sut.GetAll(page);

        // then
        result.Items.Any().Should().BeTrue();
        result.Items.Should().HaveCount(page.PageSize);
        var expectedNames = SpeciesForTest.Page(page).Select(x => x.Name);
        var actualNames = result.Items.Select(x => x.Name);
        actualNames.Should().Contain(expectedNames);

        result.PageNumber.Should().Be(page.PageNumber);
        result.PageSize.Should().Be(page.PageSize);
        result.TotalPages.Should().Be(expectedNumberOfItems / page.PageSize);
    }
}
