using Bogus;
using FluentAssertions;

namespace GardenMind.Domain.Tests;

public class SpeciesTests
{
    private static Faker faker = new();

    [Test]
    [TestCaseSource(nameof(ValidSpeciesNames))]
    public void Creating_New_Species_Succeeds_For_Valid_Name(string name)
    {
        // given
        var speciesName = name.Trim();

        // when
        var species = Species.Create(speciesName);

        // then
        species.Name.Should().Be(speciesName);
    }

    [Test]
    [TestCaseSource(nameof(InvalidSpeciesNames))]
    public void Creating_New_Species_Throws_For_Invalid_Name(string name)
    {
        // when
        Action createNewSpecies = () => Species.Create(name);

        // then
        createNewSpecies.Should().Throw<ArgumentException>();
    }

    public static IEnumerable<string> InvalidSpeciesNames()
    {
        var tooLongName = faker.Random.AlphaNumeric(129);

        yield return tooLongName;
        yield return "\t\t\t\t\t";
        yield return "\n\n\n\n\n\n\n";
        yield return "\r\n\r\n\r\n\r\n\r\n";
    }

    public static IEnumerable<string> ValidSpeciesNames()
    {
        var longName = faker.Random.AlphaNumeric(128);
        yield return "abc";
        yield return longName.ToString();
        yield return $"\t\t\t\n\n\n\n\r\r\r\r{faker.Name.FullName}";
    }
}