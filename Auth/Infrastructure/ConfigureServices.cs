﻿using Domain.Common;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDb(configuration);
        services.AddRabbitMQServices(configuration);
        services.AddRepository();

        return services;
    }

    private static IServiceCollection ConfigureDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddDbContext<ApplicationDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)),
                builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                })
            .UseSnakeCaseNamingConvention());

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddTemplateRepository<User>();
        return service;
    }

    private static void AddTemplateRepository<T>(this IServiceCollection service) where T : BaseEntitySoftDelete<T>
    {
        service.AddScoped<IRepository<T>, BaseRepository<T>>();
    }
}