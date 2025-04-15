using GardenMind.Domain.Plants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GardenMind.Persistence.Configurations;

internal class PlantConfiguration : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
        builder.ToTable("Plants");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlantedAt).IsRequired();
        builder.Property(x => x.Genus).IsRequired(false);
        builder.Property(x => x.Tag).IsRequired();

        builder.HasOne(x => x.Species).WithMany().IsRequired();
        builder.HasOne(x => x.Season).WithMany(x => x.Plants).IsRequired();

        builder.HasMany(x => x.Events).WithOne(x => x.Plant)
            .Metadata.DependentToPrincipal.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}