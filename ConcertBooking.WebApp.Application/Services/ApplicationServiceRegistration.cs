using System.Reflection;
using ConcertBooking.WebApp.Application.Behaviors;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Application.Features.Bookings.Commands.CreateBooking;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertBooking.WebApp.Application.Services;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
       //services.AddAutoMapper(_ => { }, AppDomain.CurrentDomain.GetAssemblies());
       //services.AddMediatR(cfg =>
                //cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        
        // 1. Register all FluentValidation validators of my project
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // 2. Register AutoMapper for the project
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        
        // 3. Register MediatR and attach it to the validation behavior
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // Order matters: the validation middleware will execute first.
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
       var applicationAssembly = typeof(CreateBookingCommand).Assembly;
        
        services.AddAutoMapper(_ => { }, applicationAssembly);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(applicationAssembly));

        return services;
    }
}