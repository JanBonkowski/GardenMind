using System.Runtime.CompilerServices;
using GardenMind.Domain.Plants.Events;
using GardenMind.Domain.Seasons;
using GardenMind.Domain.Seasons.Exceptions;

namespace GardenMind.Domain.Plants
{
    public class Plant
    {
        public int Id { get; init; }
        public Guid Tag { get; init; }
        public Season Season { get; init; } = default;
        public DateTime PlantedAt { get; init; }
        public Species Species { get; init; } = default;
        public string? Genus { get; init; }

        private List<PlantEvent> _plantEvents = [];
        public IReadOnlyList<PlantEvent> Events => _plantEvents;

        private Plant()
        {
        }

        public static Plant Create(Guid tag,
                                   Season season,
                                   Species species,
                                   DateTime plantedAt,
                                   string? genus = null,
                                   string? photoUri = null)
        {
            ArgumentNullException.ThrowIfNull(season, nameof(season));
            ArgumentNullException.ThrowIfNull(species, nameof(species));

            var tagIsEmpty = Guid.Empty.Equals(tag);
            if (tagIsEmpty)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (season.Terminated())
            {
                throw new SeasonAlreadyTerminatedException();
            }

            var dateWasNotSet = DateTime.MinValue.Equals(plantedAt);
            if (dateWasNotSet)
            {
                throw new ArgumentOutOfRangeException(nameof(plantedAt));
            }

            var plant = new Plant
            {
                Genus = genus,
                Tag = tag,
                PlantedAt = plantedAt,
                Season = season,
                Species = species
            };

            var plantedEvent = PlantEvent.CreatePlantedEvent(plantedAt, plant, photoUri);
            plant._plantEvents.Add(plantedEvent);

            season.AddPlant(plant);

            return plant;
        }
    }
}