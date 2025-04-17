using GardenMind.Persistence;
using GardenMind.Services.Species;
using Moq;
using Moq.EntityFrameworkCore;

namespace GardenMind.Services.Tests.Species;

public class QuerySpeciesTests
{
    private Mock<GardenDbContext> _ctx;
    private QuerySpecies _sut;

    [SetUp]
    public void Setup()
    {
        _ctx = new Mock<GardenDbContext>();
        _ctx.Setup(x => x.Species).ReturnsDbSet(Species);

        _sut = new QuerySpecies(_ctx.Object);
    }

    [Test]
    public async Task Returns_All_Species()
    {
        // when
        var result = await _sut.GetAll();

        // then
        Assert.That(result.Items.Any());
        Assert.That(result.Items.Count() == Species.Count);
    }

    private static List<Domain.Species> Species = 
        [
            Domain.Species.Create("Capsicum Annuum"),
            Domain.Species.Create("Solanum lycopersicum"),
            Domain.Species.Create("Ocimum basilicum"),
            Domain.Species.Create("Rosmarinus officinalis"),
            Domain.Species.Create("Thymus vulgaris")
        ];
}
