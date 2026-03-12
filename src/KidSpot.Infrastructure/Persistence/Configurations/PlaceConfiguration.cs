using KidSpot.Domain.Entities;
using KidSpot.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidSpot.Infrastructure.Persistence.Configurations;

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.ToTable("places");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasColumnName("description").IsRequired().HasMaxLength(2000);
        builder.Property(p => p.GooglePlaceId).HasColumnName("google_place_id").HasMaxLength(300);
        builder.Property(p => p.Type).HasColumnName("type").HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.CuratedBy).HasColumnName("curated_by");
        builder.Property(p => p.AverageRating).HasColumnName("average_rating");
        builder.Property(p => p.CreatedAt).HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");

        // Location value object → flattened columns
        builder.OwnsOne(p => p.Location, loc =>
        {
            loc.Property(l => l.Latitude).HasColumnName("lat");
            loc.Property(l => l.Longitude).HasColumnName("lng");
            loc.Property(l => l.Address).HasColumnName("address").IsRequired().HasMaxLength(500);
            loc.Property(l => l.City).HasColumnName("city").IsRequired().HasMaxLength(100);
            loc.Property(l => l.Country).HasColumnName("country").IsRequired().HasMaxLength(100);

            loc.HasIndex(l => l.City).HasDatabaseName("ix_places_city");
        });

        // KidsAttributes value object → flattened columns
        builder.OwnsOne(p => p.KidsAttributes, ka =>
        {
            ka.Property(k => k.ChangingTable).HasColumnName("changing_table");
            ka.Property(k => k.KidsMenu).HasColumnName("kids_menu");
            ka.Property(k => k.StrollerFriendly).HasColumnName("stroller_friendly");
            ka.Property(k => k.AgeFrom).HasColumnName("age_from");
            ka.Property(k => k.AgeTo).HasColumnName("age_to");
        });

        // Reviews relationship
        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Place)
            .HasForeignKey(r => r.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(p => p.Type).HasDatabaseName("ix_places_type");
    }
}
