using Application;
using Infrastructure;

namespace Api.Configurations;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddApplicationServices();
        //services.AddKafkaCommonServices(configuration);
    }
}