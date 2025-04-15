using Bogus;
using FluentAssertions;
using GardenMind.Domain.Plants.Events;
using GardenMind.Domain.Plants.Events.Details;

namespace GardenMind.Domain.Tests.Plants.Events;

public class PlantedEventTests
{
    private static Faker faker = new Faker();

    [Test]
    public void PlantedEvent_Can_Not_Be_Created_With_Default_CreatedAt()
    {
        // given
        var plant = GardeningTestingUtils.NewPlant();

        // when
        Action createPlantEvent = () => PlantEvent.CreatePlantedEvent(DateTime.MinValue, plant, null);

        // then
        createPlantEvent.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestCaseSource(nameof(PhotoUriSource))]
    public void PlantedEvent_Has_Valid_Details(string? photoUri)
    {
        // given
        var plant = GardeningTestingUtils.NewPlant();

        // when
        var plantedEvent = PlantEvent.CreatePlantedEvent(DateTime.Now, plant, photoUri);

        // then
        plantedEvent.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(30));
        plantedEvent.Type.Should().Be(EventType.Planted);
        plantedEvent.Details.Should().BeOfType<PlantedEventDetails>();
        plantedEvent.Details.CreatedAt.Should().Be(plantedEvent.CreatedAt);
        plantedEvent.Details.PhotoUri.Should().Be(photoUri);
    }

    public static IEnumerable<string?> PhotoUriSource()
    {
        yield return null;
        yield return Path.Combine("/photos", "/plants", "/events", $"{faker.Random.AlphaNumeric(10)}.jpg");
    }
}