using GardenMind.Domain.Plants.Events.Details;

namespace GardenMind.Domain.Plants.Events;

public class PlantEvent
{
    public int Id { get; init; }
    public EventType Type { get; init; }
    public DateTime CreatedAt { get; init; }
    public EventDetails Details { get; init; }

    private PlantEvent()
    {
    }

    public static PlantEvent CreatePlantedEvent(DateTime createdAt, string? photoUri = null)
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
            Details = eventDetails
        };
    }
}