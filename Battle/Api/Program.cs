using Api.Extension;
using Api.Interceptors;
using Api.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCodeFirstGrpc(options =>
{
    options.Interceptors.Add<ApiExceptionInterceptor>();
    options.Interceptors.Add<UserContextInterceptor>();
});
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BattleService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.StartMigrationDb();

app.Run();
