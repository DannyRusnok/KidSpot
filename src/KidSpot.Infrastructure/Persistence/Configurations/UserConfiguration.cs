using KidSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidSpot.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id");
        builder.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(300);
        builder.Property(u => u.Name).HasColumnName("name").IsRequired().HasMaxLength(200);
        builder.Property(u => u.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(500);
        builder.Property(u => u.GoogleId).HasColumnName("google_id").IsRequired().HasMaxLength(100);
        builder.Property(u => u.IsAdmin).HasColumnName("is_admin");
        builder.Property(u => u.CreatedAt).HasColumnName("created_at");

        builder.HasIndex(u => u.GoogleId).IsUnique().HasDatabaseName("ix_users_google_id");
        builder.HasIndex(u => u.Email).IsUnique().HasDatabaseName("ix_users_email");

        // Ignore the navigation — saved places go through SavedPlace join entity
        builder.Ignore(u => u.SavedPlaces);
    }
}
