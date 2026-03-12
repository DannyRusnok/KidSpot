using KidSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidSpot.Infrastructure.Persistence;

public class KidSpotDbContext : DbContext
{
    public DbSet<Place> Places => Set<Place>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<SavedPlace> SavedPlaces => Set<SavedPlace>();

    public KidSpotDbContext(DbContextOptions<KidSpotDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KidSpotDbContext).Assembly);
    }
}
