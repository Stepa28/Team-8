using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Configuration;
using RabbitMQ.Producer;

namespace RabbitMQ;

public static class ConfigureServices
{
    public static IServiceCollection AddRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.GetSection(RabbitMQOptions.SectionKey).Get<RabbitMQOptions>();
        ArgumentNullException.ThrowIfNull(option, nameof(option));

        services.AddScoped<IUserDeleteProducer, UserDeleteProducer>();
        services.AddScoped<IUserAddProducer, UserAddProducer>();
        
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure(options =>
            {
                options.Host = option.RabbitMQHost;
                options.Port = ushort.Parse(option.Port);
                options.User = option.Username;
                options.Pass = option.Password;
            });
        services.AddMassTransit(x => x.UsingRabbitMq());

        return services;
    }
}