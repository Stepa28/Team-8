using Domain.Interfaces;
using gRPC;
using gRPC.Configuration;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ;
using RabbitMQ.Configuration;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceOptions>(configureOptions => configuration.GetSection(nameof(ServiceOptions)).Bind(configureOptions));
        services.Configure<RabbitMQOptions>(configureOptions => configuration.GetSection(nameof(RabbitMQOptions)).Bind(configureOptions));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoomService, RoomService>();

        services.ConfigureGrpcClients(configuration);
        services.AddRabbitMQServices(configuration);
        
        return services;
    }
}