namespace GardenMind.Services.Species.Models;

public record SpeciesListItem(int Id, string Name)
{
    internal static SpeciesListItem From(Domain.Species domainObject)
    {
        return new SpeciesListItem(domainObject.Id, domainObject.Name);
    }
}