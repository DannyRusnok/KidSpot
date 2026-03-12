using KidSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidSpot.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id");
        builder.Property(r => r.PlaceId).HasColumnName("place_id");
        builder.Property(r => r.UserId).HasColumnName("user_id");
        builder.Property(r => r.Rating).HasColumnName("rating");
        builder.Property(r => r.Text).HasColumnName("text").IsRequired().HasMaxLength(2000);
        builder.Property(r => r.CreatedAt).HasColumnName("created_at");

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.PlaceId).HasDatabaseName("ix_reviews_place_id");
    }
}
