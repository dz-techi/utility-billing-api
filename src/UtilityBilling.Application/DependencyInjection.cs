using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Application.Services;

namespace UtilityBilling.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapster();
        
        services.AddScoped<IDataSeedingService, DataSeedingService>();

        // Automatically scan for all mapping configurations. 
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}