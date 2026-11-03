using ConcertBooking.WebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> entity)
    {
        entity.HasKey(v => v.Id);
        entity.Property(v => v.Name).IsRequired().HasMaxLength(100);
        entity.Property(v => v.Address).IsRequired().HasMaxLength(200);
        entity.Property(v => v.SeatCapacity).IsRequired();

        entity.HasMany(v => v.Concerts)
            .WithOne(c => c.Venue)
            .HasForeignKey(c => c.VenueId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // insert seed data with HasData();
    }
}