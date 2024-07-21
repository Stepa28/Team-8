using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Interfaces;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Team_8.Contracts.DTOs;

namespace Api.Interceptors;

public class UserContextInterceptor(ILogger<UserContextInterceptor> logger, IUserContext userContext) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        userContext.Context = context;
        var value = context.RequestHeaders.GetValue("UserContext");
        if(string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException("В заголовкe UserContext, должен присутствовать сериализованном объект UserDto");
        userContext.User = JsonSerializer.Deserialize<UserDto>(value,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString });

        return await continuation(request, context);
    }
}