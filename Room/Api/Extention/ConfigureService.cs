using Application;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Extention;

public static class ConfigureService
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        return services;
    }
    
    public static void StartMigrationDb(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        using var context = (ApplicationDbContext)serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        context.Database.Migrate();
    }
}