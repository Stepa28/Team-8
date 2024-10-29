using Domain.Interfaces.Producers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Configuration;
using RabbitMQ.Consumer;
using RabbitMQ.Producer;
using Team_8.Contracts.Enums;

namespace RabbitMQ;

public static class ConfigureServices
{
    public static IServiceCollection AddRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.GetSection(RabbitMQOptions.SectionKey).Get<RabbitMQOptions>();
        ArgumentNullException.ThrowIfNull(option, nameof(option));

        services.AddScoped<IBattleStatusProducer, BattleStatusProducer>();
        services.AddScoped<IBattleStateProducer, BattleStateProducer>();
        
        services.AddOptions<RabbitMqTransportOptions>()
            .Configure(options =>
            {
                options.Host = option.RabbitMQHost;
                options.Port = ushort.Parse(option.Port);
                options.User = option.Username;
                options.Pass = option.Password;
            });
            
        services.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(ConfigureServices).Assembly);
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint($"{nameof(CreateBattleConsumer)}_{Microservice.Battle.ToString()}",
                    e =>
                    {
                        e.ConfigureConsumer<CreateBattleConsumer>(context);
                    });
            });
        });

        return services;
    }
}