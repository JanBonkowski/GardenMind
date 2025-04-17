using Bogus;
using GardenMind.Domain.Plants;
using GardenMind.Domain.Seasons;

namespace GardenMind.SharedTest;

public class GardeningTestingUtils
{
    private static Faker faker = new Faker();

    public static Season NewPlannedSeason()
    {
        return NewSeason(SeasonStatus.Planned);
    }

    public static Season NewStartedSeason()
    {
        return NewSeason(SeasonStatus.Started);
    }

    public static Season NewTerminatedSeason()
    {
        return NewSeason(SeasonStatus.Terminated);
    }

    private static Season NewSeason(SeasonStatus status)
    {
        var season = Season.Create();
        switch (status)
        {
            case SeasonStatus.Started:
                {
                    season.Start();
                    return season;
                }
            case SeasonStatus.Terminated:
                {
                    season.Start();
                    season.Terminate();
                    return season;
                }
        }
        return season;
    }

    public static Plant NewPlant()
    {
        var tag = Guid.NewGuid();
        var species = SpeciesTestingUtils.NewSpecies();
        var season = NewPlannedSeason();

        return Plant.Create(tag, season, species, DateTime.UtcNow);
    }
}