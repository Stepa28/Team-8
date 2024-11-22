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

        services.AddScoped<ICreateBattleProducer, CreateBattleProducer>();
        services.AddScoped<IUserJoinLeaveProducer, UserJoinLeaveProducer>();
        services.AddScoped<IRemoveRoomProducer, RemoveRoomProducer>();
        services.AddScoped<IAddOrUpdateRoomProducer, AddOrUpdateRoomProducer>();
        
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
                cfg.ReceiveEndpoint($"{nameof(UserAddConsumer)}_{Microservice.Room.ToString()}",
                    e =>
                    {
                        e.ConfigureConsumer<UserAddConsumer>(context);
                    }); 
                
                cfg.ReceiveEndpoint($"{nameof(UserDeleteConsumer)}_{Microservice.Room.ToString()}",
                    e =>
                    {
                        e.ConfigureConsumer<UserDeleteConsumer>(context);
                    });
                
                cfg.ReceiveEndpoint($"{nameof(BattleStatusConsumer)}_{Microservice.Room.ToString()}",
                    e =>
                    {
                        e.ConfigureConsumer<BattleStatusConsumer>(context);
                    });
            });
        });

        return services;
    }
}