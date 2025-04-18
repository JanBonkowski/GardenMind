namespace GardenMind.Domain.Seasons.Exceptions;

public class SeasonPendingException : Exception
{
    public SeasonPendingException() : base("There is already a pending season")
    {
    }
}