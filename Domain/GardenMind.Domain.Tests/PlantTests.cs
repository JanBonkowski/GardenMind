using Bogus;
using FluentAssertions;
using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;

namespace GardenMind.Domain.Tests;

public class PlantTests
{
    private static Faker faker = new Faker();
    private Guid tag = Guid.NewGuid();
    private Species species = null;
    private Season season = null;

    [SetUp]
    public void SetUp()
    {
        tag = Guid.NewGuid();
        species = GardeningTestingUtils.NewSpecies();
        season = GardeningTestingUtils.NewPlannedSeason();
    }

    [Test]
    public void Plant_Tag_Can_Not_Be_Empty()
    {
        // given
        var tag = Guid.Empty;

        // when
        Action createPlant = () => Plant.Create(tag, season, species, DateTime.Now);

        // then
        createPlant.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Plant_Planted_At_Date_Can_Not_Be_Default()
    {
        // when
        Action createPlant = () => Plant.Create(tag, season, species, DateTime.MinValue);

        // then
        createPlant.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Plant_Can_Not_Be_Created_After_Season_Terminated()
    {
        // given
        var alreadyTerminatedSeason = GardeningTestingUtils.NewTerminatedSeason();

        // when
        Action createPlant = () => Plant.Create(tag, alreadyTerminatedSeason, species, DateTime.Now);

        // then
        createPlant.Should().Throw<SeasonAlreadyTerminatedException>();
    }

    [TestCaseSource(nameof(ValidPlantedAtDates))]
    public void Plant_Is_Created_Successfully_For_Valid_Planted_At_Date(DateTime plantedAt)
    {
        // when
        var plant = Plant.Create(tag, season, species, plantedAt);

        // then
        plant.Tag.Should().Be(tag);
        plant.Season.Should().Be(season);
        plant.Species.Should().Be(species);
        plant.PlantedAt.Should().Be(plantedAt);
        plant.Genus.Should().BeNull();
    }

    [Test]
    public void Plant_Is_Created_Successfully_For_Valid_Genus()
    {
        // given
        var genus = faker.Name.FirstName();

        // when
        var plant = Plant.Create(tag, season, species, DateTime.Now, genus);

        // then
        plant.Tag.Should().Be(tag);
        plant.Season.Should().Be(season);
        plant.Species.Should().Be(species);
        plant.PlantedAt.Should().NotBe(DateTime.MinValue);
        plant.Genus.Should().Be(genus);
    }

    public static IEnumerable<DateTime> ValidPlantedAtDates()
    {
        yield return DateTime.Now;
        yield return DateTime.Now.AddDays(31);
    }
}