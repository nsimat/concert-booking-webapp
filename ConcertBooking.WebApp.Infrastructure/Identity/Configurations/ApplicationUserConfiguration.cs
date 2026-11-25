using ConcertBooking.WebApp.Domain.Entities;
using ConcertBooking.WebApp.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertBooking.WebApp.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> entity)
    {
        /*entity.HasKey(au => au.Id);
        entity.Property(au => au.UserName)
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(au => au.Email)
            .IsRequired()
            .HasMaxLength(50);*/
    }
}