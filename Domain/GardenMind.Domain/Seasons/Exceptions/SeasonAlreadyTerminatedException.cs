namespace GardenMind.Domain.Seasons.Exceptions;

public class SeasonAlreadyTerminatedException : Exception
{
    public SeasonAlreadyTerminatedException() : base("Season was already terminated")
    {
    }
}