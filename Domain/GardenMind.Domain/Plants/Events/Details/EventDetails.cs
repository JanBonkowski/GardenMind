namespace GardenMind.Domain.Plants.Events.Details;

public abstract record EventDetails(EventType Type,
                                    DateTime CreatedAt,
                                    string? PhotoUri)
{
}