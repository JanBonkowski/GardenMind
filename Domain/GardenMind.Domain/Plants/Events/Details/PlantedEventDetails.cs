namespace GardenMind.Domain.Plants.Events.Details;

public record PlantedEventDetails : EventDetails
{
    private PlantedEventDetails() : base(EventType.Planted, DateTime.Now, null)
    {
    }

    private PlantedEventDetails(DateTime createdAt,
                               string? photoUri) : base(EventType.Planted, createdAt, photoUri)
    {
    }

    public static PlantedEventDetails WithPhoto(DateTime createdAt, string photoUri)
    {
        return new PlantedEventDetails(createdAt, photoUri);
    }

    public static PlantedEventDetails WithoutPhoto(DateTime createdAt)
    {
        return new PlantedEventDetails(createdAt, null);
    }
}