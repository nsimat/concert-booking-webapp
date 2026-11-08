using ConcertBooking.WebApp.Application.Services;
using ConcertBooking.WebApp.Infrastructure.Services;
using ConcertBooking.WebApp.Persistence.Services;

namespace ConcertBooking.WebApp.Api;

public static class StartupExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddProblemDetails();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        return builder;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.MapOpenApi();
        app.MapControllers();

        return app;
    }
    
}