using System.Reflection;
using Application.Common.Attributes;
using Domain.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Team_8.Contracts.Enums;

namespace Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(IUserContext context) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttribute<AuthorizeAttribute>();

        if(authorizeAttributes == null) return await next();
        var userRole = context.User.Role;
        if(context.User.Id == Guid.Empty || userRole == Role.None)
            throw new UnauthorizedAccessException();

        var role = authorizeAttributes.Role;
        if(role != Role.None && (userRole & role) == 0)
            throw new ForbiddenAccessException();

        return await next();
    }
}