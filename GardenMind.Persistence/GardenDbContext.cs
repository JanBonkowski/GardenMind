using GardenMind.Domain;
using GardenMind.Domain.Seasons;
using GardenMind.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GardenMind.Persistence;

public class GardenDbContext : DbContext
{
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Species> Species { get; set; }

    public GardenDbContext(DbContextOptions options) : base(options)
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
    }
}