using System.Reflection;
using ConcertBooking.WebApp.Application.Behaviors;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertBooking.WebApp.Application.Services;

/// <summary>
/// Provides application-layer service registration for the dependency injection container.
/// </summary>
/// <remarks>
/// Registers AutoMapper mappings, MediatR handlers and pipeline behaviors, and FluentValidation validators from the application assembly. Configures validation to run through the MediatR request pipeline before request handlers are executed.
/// </remarks>
public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // 1. Add AutoMapper
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        
        // 2. Add MediatR and attach it to the validation behavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // Order matters: the validation middleware will execute first.
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        // 3. Add FluentValidation validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // 4. Add validation pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Redondance: already added in MediatR configuration above, but kept for clarity.
        
       /*var applicationAssembly = typeof(CreateBookingCommand).Assembly;        
        services.AddAutoMapper(_ => { }, applicationAssembly);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(applicationAssembly));*/

        return services;
    }
}