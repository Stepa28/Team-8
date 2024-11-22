using Domain.Common.Configuration;
using Domain.Interfaces;
using Domain.Options;
using gRPC;
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
        services.Configure<GrpsOptions>(configureOptions => configuration.GetSection(nameof(GrpsOptions)).Bind(configureOptions));
        services.Configure<RabbitMQOptions>(configureOptions => configuration.GetSection(nameof(RabbitMQOptions)).Bind(configureOptions));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoomService, RoomService>();

        services.AddOptionsWithValidateOnStart<WebSocketOption>()
            .Bind(configuration.GetSection(WebSocketOption.SectionKey));

        services.ConfigureGrpcClients(configuration);
        services.AddRabbitMQServices(configuration);
        
        return services;
    }
}