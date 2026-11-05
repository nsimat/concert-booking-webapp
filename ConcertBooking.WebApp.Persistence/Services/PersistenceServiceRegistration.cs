using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertBooking.WebApp.Persistence.Services;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ConcertBookingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConcertBookingManagementConnectionString")));
        
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IConcertRepository, ConcertRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IVenueRepository, VenueRepository>();

        return services;
    }
    
}