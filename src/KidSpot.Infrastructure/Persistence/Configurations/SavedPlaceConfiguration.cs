using KidSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidSpot.Infrastructure.Persistence.Configurations;

public class SavedPlaceConfiguration : IEntityTypeConfiguration<SavedPlace>
{
    public void Configure(EntityTypeBuilder<SavedPlace> builder)
    {
        builder.ToTable("saved_places");

        builder.HasKey(sp => new { sp.UserId, sp.PlaceId });
        builder.Property(sp => sp.UserId).HasColumnName("user_id");
        builder.Property(sp => sp.PlaceId).HasColumnName("place_id");
        builder.Property(sp => sp.SavedAt).HasColumnName("saved_at");

        builder.HasOne(sp => sp.User)
            .WithMany()
            .HasForeignKey(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sp => sp.Place)
            .WithMany()
            .HasForeignKey(sp => sp.PlaceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
