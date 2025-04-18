namespace GardenMind.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string name) : base($"Could not find {name} with specified id in database")
        {
        }
    }
}