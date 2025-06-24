using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UtilityBilling.Infrastructure.Database;
using UtilityBilling.Infrastructure.Repositories;
using UtilityBilling.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UtilityBilling.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configurationManager.GetSection("PostgresDB:ConnectionString").Value));
        
        services.AddScoped<IUtilityBillPeriodRepository, UtilityBillPeriodRepository>();
        
        return services;
    }
}