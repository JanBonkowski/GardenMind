using GardenMind.Domain.Plants;

namespace GardenMind.SharedTest;

public class PlantsTestingUtils
{
    public static Plant NewPlant()
    {
        var tag = Guid.NewGuid();
        var species = SpeciesTestingUtils.NewSpecies();
        var season = SeasonsTestingUtils.NewPlannedSeason();

        return Plant.Create(tag, season, species, DateTime.UtcNow);
    }
}