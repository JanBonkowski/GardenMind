using Bogus;
using GardenMind.Domain.Seasons;

namespace GardenMind.Domain.Tests;

public class GardeningTestingUtils
{
    private static Faker faker = new Faker();

    public static Species NewSpecies()
    {
        return NewSpecies(faker.Random.AlphaNumeric(125));
    }

    public static Species NewSpecies(string name)
    {
        return Species.Create(name);
    }

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
}