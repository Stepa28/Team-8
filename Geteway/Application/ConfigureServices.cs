using System.Reflection;
using Application.Common.Behaviors;
using Application.Common.WebSocket;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly executionAssembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(executionAssembly);
        services.AddSingleton<IWebSocketConnections, WebSocketConnections>();
        services.AddScoped<IContext, Context>();
        services.AddSingleton<IMessageQuery, MessageQuery>(x=>new MessageQuery(x.GetService<ILogger<MessageQuery>>(), x));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(executionAssembly);

            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}