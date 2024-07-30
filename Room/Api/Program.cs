using Api.Extension;
using Api.Interceptors;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<ApiExceptionInterceptor>();
    options.Interceptors.Add<UserContextInterceptor>();
});

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<RoomServices>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.StartMigrationDb();
app.MapGrpcReflectionService();

app.Run();
