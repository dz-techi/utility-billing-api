using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using UtilityBilling.Api.Exceptions;
using UtilityBilling.Api.Services;
using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Infrastructure.Database;

namespace UtilityBilling.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddOpenTelemetry()
            .ConfigureResource(res => res.AddService("UtilityBilling.Api"))
            .WithMetrics(m =>
            {
                m.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                m.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:18889");
                });
            })
            .WithTracing(t =>
            {
                t.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
        
                t.AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:18889");
                });
            });
        
        services.AddCors();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = "https://dev-7yg7bk42kt1hapxi.us.auth0.com/";
            options.Audience = "http://localhost:5299";

            // options.TokenValidationParameters = new TokenValidationParameters
            // {
            //     ValidateAudience = false,
            //     ValidateIssuer = false, // Temporarily disable issuer validation for debugging
            //     ValidateIssuerSigningKey = true,
            // };
            
            // options.Events = new JwtBearerEvents
            // {
            //     OnAuthenticationFailed = context =>
            //     {
            //         Console.WriteLine("Authentication failed: " + context.Exception.Message);
            //         return Task.CompletedTask;
            //     },
            //     OnTokenValidated = context =>
            //     {
            //         Console.WriteLine("Token validated: " + context.SecurityToken);
            //         return Task.CompletedTask;
            //     },
            //     OnChallenge = context =>
            //     {
            //         Console.WriteLine("OnChallenge error: " + context.Error + ", description: " + context.ErrorDescription);
            //         return Task.CompletedTask;
            //     }
            // };
        });

        services.AddAuthorization();

        return services;
    }
}