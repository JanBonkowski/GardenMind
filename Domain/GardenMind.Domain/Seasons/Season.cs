using GardenMind.Domain.Plants;
using GardenMind.Domain.Seasons.Exceptions;

namespace GardenMind.Domain.Seasons;

public partial class Season
{
    public int Id { get; private set; } = default;
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? TerminatedAt { get; private set; }

    private HashSet<Plant> _plants = [];
    public IReadOnlySet<Plant> Plants { get => _plants; }

    public static Season Create()
    {
        return new Season() { CreatedAt = DateTime.Now };
    }

    public void Start()
    {
        if (Started())
        {
            throw new SeasonAlreadyStartedException();
        }

        if (Terminated())
        {
            throw new SeasonAlreadyTerminatedException();
        }

        StartedAt = DateTime.Now;
    }

    public bool Started() => StartedAt.HasValue && !TerminatedAt.HasValue;

    public bool Terminated() => TerminatedAt.HasValue;

    public void Terminate()
    {
        if (Terminated())
        {
            throw new SeasonAlreadyTerminatedException();
        }

        if (!Started())
        {
            throw new SeasonNotStartedException();
        }


        TerminatedAt = DateTime.Now;
    }

    internal void AddPlant(Plant plant)
    {
        if (Terminated())
        {
            throw new SeasonAlreadyTerminatedException();
        }

        _plants.Add(plant);
    }

    public SeasonStatus Status
    {
        get
        {
            if (!Started() && !Terminated())
            {
                return SeasonStatus.Planned;
            }

            if (Started() && !Terminated())
            {
                return SeasonStatus.Started;
            }

            return SeasonStatus.Terminated;
        }
    }
}