using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly executionAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(executionAssembly);
        services.AddValidatorsFromAssembly(executionAssembly);

        return services;
    }
}