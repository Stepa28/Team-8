using gRPC.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Team8.Contracts.Auth.Service;

namespace gRPC;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.GetSection(ServiceOptions.SectionKey).Get<ServiceOptions>();
        Log.Information(option.Shema + option.Auth);
        services.AddGrpcClient<AuthService.AuthServiceClient>(o =>
        {
            o.Address = new Uri(option.Shema + option.Auth);
        });
        
        return services;
    }
}