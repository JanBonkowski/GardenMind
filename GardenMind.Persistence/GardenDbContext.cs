using GardenMind.Domain;
using GardenMind.Domain.Plants;
using GardenMind.Domain.Seasons;
using GardenMind.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Persistence;

public class GardenDbContext : DbContext
{
    public virtual DbSet<Plant> Plants { get; set; }
    public virtual DbSet<Season> Seasons { get; set; }
    public virtual DbSet<Species> Species { get; set; }

    public GardenDbContext(DbContextOptions<GardenDbContext> options) : base(options)
    {
    }

    protected GardenDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new SpeciesConfiguration());
        modelBuilder.ApplyConfiguration(new SeasonConfiguration());
        modelBuilder.ApplyConfiguration(new PlantConfiguration());
        modelBuilder.ApplyConfiguration(new PlantEventConfiguration());
    }
}