using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> entity)
    {
        entity.HasKey(a => a.Id);
        entity.Property(a => a.StageName)
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(a => a.Bio)
            .HasMaxLength(500);
        entity.Property(a => a.MusicalStyle)
            .HasConversion<string>()
            .IsRequired();
        entity.Property(a => a.ImageUrl)
            .HasMaxLength(200);

        entity.HasMany(a => a.Concerts)
            .WithOne(c => c.Artist)
            .HasForeignKey(c => c.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed data with HasData()
    }
}