using GardenMind.Domain.Plants.Events.Details;

namespace GardenMind.Domain.Plants.Events;

public class PlantEvent
{
    public int Id { get; private set; }
    public EventType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public EventDetails? Details { get; private set; }
    public Plant? Plant { get; private set; }

    private PlantEvent()
    {
    }

    public static PlantEvent CreatePlantedEvent(DateTime createdAt, Plant plant, string? photoUri = null)
    {
        var createdAtIsUnspecified = createdAt == DateTime.MinValue;
        if (createdAtIsUnspecified)
        {
            throw new ArgumentOutOfRangeException(nameof(createdAt));
        }

        var eventDetails = photoUri switch
        {
            string uri => PlantedEventDetails.WithPhoto(createdAt, uri),
            null => PlantedEventDetails.WithoutPhoto(createdAt)
        };

        return new PlantEvent
        {
            CreatedAt = createdAt,
            Type = eventDetails.Type,
            Details = eventDetails,
            Plant = plant
        };
    }
}