namespace GardenMind.Domain.Seasons.Exceptions;

public class SeasonNotStartedException : Exception
{
    public SeasonNotStartedException() : base("Season was not started")
    {
    }
}