using Api.Configurations;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    Log.Logger = builder.AddLogger().ForContext<Program>();
    
    builder.Services.AddApiService();
    builder.Services.AddServices(builder.Configuration);

    var app = builder.Build();

    app.UseSwaggerSetup();
    app.MapControllers();
    app.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "error message");
}
finally
{
    Log.Information("Выключение завершено");
    await Log.CloseAndFlushAsync();
}