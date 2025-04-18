using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using GardenMind.Domain;
using GardenMind.Persistence;
using GardenMind.Services.Species;
using GardenMind.Services.Species.Models;
using GardenMind.SharedTest;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Species;

public class SpeciesCreatorTests
{
    private Mock<GardenDbContext> _ctx;
    private SpeciesCreator _sut;

    private List<Domain.Species> _species = [];

    [SetUp]
    public void Setup()
    {
        _species = [];
        _ctx = new Mock<GardenDbContext>();
        _ctx.Setup(x => x.Species).ReturnsDbSet(_species);
        _ctx.Setup(x => x.Species.Add(It.IsAny<Domain.Species>())).Callback<Domain.Species>((entry) =>
        {
            _species.Add(entry);
        });
        _ctx.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() =>
        {
            var insertedSpecies = _ctx.Object.Species.First();
            var field = typeof(Domain.Species).GetProperty(nameof(Domain.Species.Id));
            field!.SetValue(insertedSpecies, Random.Shared.Next(0, 100));

            return Task.FromResult(1);
        });

        _sut = new SpeciesCreator(_ctx.Object);
    }

    [Test]
    public async Task Creates_Species_Successfully()
    {
        // given
        var name = SpeciesTestingUtils.NewSpecies().Name;
        var request = new CreateSpeciesRequest(name);

        // when
        var result = await _sut.Create(request);

        // then
        result.Should().BeGreaterThan(0);
    }
}
