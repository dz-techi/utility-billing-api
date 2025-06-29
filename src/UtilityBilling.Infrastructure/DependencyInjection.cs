using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtilityBilling.Domain.Models;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories;
using UtilityBilling.Infrastructure.Repositories.Interfaces;

namespace UtilityBilling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetSection("PostgresDB:ConnectionString").Value;

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("PostgreSQL connection string is not configured. Please check your appsettings.json file.");
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });

            // Enable detailed error messages in development
            // if (configurationManager.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development")
            // {
            //     options.EnableSensitiveDataLogging();
            //     options.EnableDetailedErrors();
            // }
        });

        services.AddScoped<IUtilityBillPeriodRepository, UtilityBillPeriodRepository>();
        // services.AddScoped<IBaseRepository<ProductDto>, BaseRepository<ProductDto>>();

        return services;
    }
}