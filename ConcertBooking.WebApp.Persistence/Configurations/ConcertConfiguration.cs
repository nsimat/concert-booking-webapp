using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
{
    public void Configure(EntityTypeBuilder<Concert> entity)
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Title).IsRequired().HasMaxLength(100);
        entity.Property(c => c.Description).HasMaxLength(200);
        entity.Property(c => c.ImageUrl).HasMaxLength(200);
        entity.Property(c => c.Status).HasConversion<string>().IsRequired();
        entity.Property(c => c.DateTime).IsRequired();

        entity.HasOne(c => c.Venue)
            .WithMany(v => v.Concerts)
            .HasForeignKey(c => c.VenueId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(c => c.Artist)
            .WithMany(a => a.Concerts)
            .HasForeignKey(c => c.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(c => c.Bookings)
            .WithOne(b => b.Concert)
            .HasForeignKey(b => b.ConcertId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed data with HasData()
    }
}