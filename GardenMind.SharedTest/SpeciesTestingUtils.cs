using Bogus;
using GardenMind.Domain;

namespace GardenMind.SharedTest
{
    public static class SpeciesTestingUtils
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

        public static ISet<Species> MultipleSpecies(int number)
        {
            var species = new HashSet<Species>();

            foreach (var _ in Enumerable.Range(0, number))
            {
                species.Add(NewSpecies());
            }

            return species;
        }
    }
}