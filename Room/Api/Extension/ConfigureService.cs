using Api.Managers;
using Application;
using Domain.Interfaces;
using Infrastructure;

namespace Api.Extension;

public static class ConfigureService
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserContext, UserContext>();
        
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        return services;
    }
}