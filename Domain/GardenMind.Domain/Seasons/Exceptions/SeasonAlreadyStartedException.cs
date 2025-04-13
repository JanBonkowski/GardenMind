namespace GardenMind.Domain.Seasons.Exceptions;

public class SeasonAlreadyStartedException : Exception
{
    public SeasonAlreadyStartedException() : base("Season was already started")
    {
    }
}