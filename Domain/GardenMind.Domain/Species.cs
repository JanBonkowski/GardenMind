namespace GardenMind.Domain;

public class Species
{
    public const int MAX_NAME_LENGTH = 128;
    public const int MIN_NAME_LENGTH = 3;

    public int Id { get; private set; } = default;

    public string Name { get; private set; } = string.Empty;

    private Species()
    {
    }

    public static Species Create(string name)
    {
        name = name.Trim();

        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        if (name.Length > MAX_NAME_LENGTH)
        {
            throw new ArgumentException($"Species name must not exceed {MAX_NAME_LENGTH} characters.", nameof(name));
        }

        if (name.Length < MIN_NAME_LENGTH)
        {
            throw new ArgumentException($"Species name must be longer than {MIN_NAME_LENGTH} characters.", nameof(name));
        }

        return new Species
        {
            Name = name
        };
    }
}