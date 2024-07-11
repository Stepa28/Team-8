using Domain.Interfaces;
using gRPC;
using gRPC.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceOptions>(configureOptions => configuration.GetSection(nameof(ServiceOptions)).Bind(configureOptions));
        services.AddScoped<IAuthService, AuthService>();

        services.ConfigureGrpcClients(configuration);
        
        return services;
    }
}