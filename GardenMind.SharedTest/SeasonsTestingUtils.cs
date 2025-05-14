using GardenMind.Domain.Seasons;

namespace GardenMind.SharedTest;

public class SeasonsTestingUtils
{
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
    public static void SetRandomId(Season season)
    {
        var field = typeof(Season).GetProperty(nameof(Season.Id));
        field!.SetValue(season, Random.Shared.Next(0, 100));
    }
}