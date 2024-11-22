using Domain.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Team8.Contracts.Auth.Service;
using Team8.Contracts.Room.Service;

namespace gRPC;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var option = configuration.GetSection(GrpsOptions.SectionKey).Get<GrpsOptions>();
        Log.Information(option.Shema + option.Auth);
        
        services.Configure<GrpsOptions>(configuration.GetSection(GrpsOptions.SectionKey));

        services.AddGrpcClient<AuthService.AuthServiceClient>(o =>
        {
            o.Address = new Uri(option.Shema + option.Auth);
        });

        services.AddGrpcClient<RoomService.RoomServiceClient>(o =>
        {
            o.Address = new Uri(option.Shema + option.Room);
        });

        return services;
    }
}