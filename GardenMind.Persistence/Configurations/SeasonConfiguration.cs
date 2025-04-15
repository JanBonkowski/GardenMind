using GardenMind.Domain.Seasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GardenMind.Persistence.Configurations;

internal class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("Seasons");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.StartedAt).IsRequired(false);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.TerminatedAt).IsRequired(false);

        builder.HasMany(x => x.Plants).WithOne(x => x.Season)
            .Metadata.DependentToPrincipal.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}