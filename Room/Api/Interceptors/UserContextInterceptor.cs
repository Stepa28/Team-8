using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Team_8.Contracts.ConstStrings;
using Team_8.Contracts.DTOs;

namespace Api.Interceptors;

public class UserContextInterceptor(ILogger<UserContextInterceptor> logger, IUserContext userContext, IUserRepository repository) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        userContext.Context = context;
        var value = context.RequestHeaders.GetValue(RpcHeaders.UserContext);
        if(string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException($"В заголовке {RpcHeaders.UserContext}, должен присутствовать сериализованном объект {nameof(UserDto)}");
        userContext.User = JsonSerializer.Deserialize<UserDto>(value,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString });
        
        if(await repository.GetAsync(userContext.User.Id) == null)
            throw new NotFoundException("Вас нет в этой БД этого микросервиса");
        
        logger.LogDebug("В контекст добавлена информацио о пользователе {@UserId}", userContext.User.Id);

        return await continuation(request, context);
    }
}