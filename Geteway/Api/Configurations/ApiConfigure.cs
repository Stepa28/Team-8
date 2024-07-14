using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;
using Api.Managers;
using Domain.Interfaces;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

namespace Api.Configurations;

public static class ApiConfigure
{
    public static void AddApiService(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IAuthInfoProvider, AuthInfoProvider>();
        
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.EnableAnnotations();
            opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme
                , new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header
                    , Description = "Please enter token"
                    , Name = "Authorization"
                    , Type = SecuritySchemeType.Http
                    , BearerFormat = "JWT"
                    , Scheme = JwtBearerDefaults.AuthenticationScheme
                });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }
                    , Array.Empty<string>()
                }
            });

            opt.SchemaFilter<FluentValidationRulesScopeAdapter>(ServiceLifetime.Scoped);
            opt.OperationFilter<FluentValidationOperationFilterScopeAdapter>(ServiceLifetime.Scoped);
        });
        
        services.AddFluentValidationRulesToSwagger();
    }

    public static void UseSwaggerSetup(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                Log.Error(context.Features.Get<IExceptionHandlerPathFeature>()?.Error, "Необработанное исключение.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await Task.CompletedTask;
            });
        });

        app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseWebSockets();
    }
}