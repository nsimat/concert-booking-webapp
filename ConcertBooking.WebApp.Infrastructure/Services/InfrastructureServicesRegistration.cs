using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertBooking.WebApp.Infrastructure.Services;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, IEmailService>();

        return services;
    }
    
}