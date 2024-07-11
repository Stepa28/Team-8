using gRPC.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Team8.Contracts.Auth.Service;

namespace gRPC;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<AuthService.AuthServiceClient>(o =>
        {
            o.Address = new Uri(configuration.GetSection(ServiceOptions.SectionKey).Get<ServiceOptions>().Auth);
        });
        
        return services;
    }
}