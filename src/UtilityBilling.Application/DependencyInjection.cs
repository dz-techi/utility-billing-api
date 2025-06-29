using System.Reflection;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace UtilityBilling.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapster();

        // Automatically scan for all mapping configurations. 
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}