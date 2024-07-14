using Serilog;

namespace Api.Configurations;

public static class LoggerConfigure
{
    public static Serilog.ILogger AddLogger(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        return new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();
    }
    
    public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
    {
        Serilog.Debugging.SelfLog.Enable(Console.Error);

        hostBuilder.UseSerilog((ctx, options) =>
        {
            options.ReadFrom.Configuration(ctx.Configuration);
        });

        return hostBuilder;
    }
    
    public static WebApplicationBuilder LogStartUp(this WebApplicationBuilder builder)
    {
        Log.Information("Starting up");
        string env = builder.Environment.EnvironmentName;
        Log.Information($"Environment ASPNETCORE_ENVIRONMENT: {env}");

        return builder;
    }
}