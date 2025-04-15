using System.Text.Json;
using GardenMind.Domain.Plants.Events;
using GardenMind.Domain.Plants.Events.Details;
using GardenMind.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GardenMind.Persistence.Configurations;

internal class PlantEventConfiguration : IEntityTypeConfiguration<PlantEvent>
{
    public void Configure(EntityTypeBuilder<PlantEvent> builder)
    {
        builder.ToTable("PlantEvents");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).IsRequired()
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<EventType>(x)
            );

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Details).IsRequired()
            .HasConversion(x => JsonSerializer.Serialize(x, JsonConverterOptions.JsonSerializerOptions),
            x => JsonSerializer.Deserialize<EventDetails>(x, JsonConverterOptions.JsonSerializerOptions)!);
    }
}