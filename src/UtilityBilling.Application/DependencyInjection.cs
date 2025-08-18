using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;
using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Application.Jobs;
using UtilityBilling.Application.Services;

namespace UtilityBilling.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapster();

        services.AddScoped<IDataSeedingService, DataSeedingService>();
        services.AddScoped<IUtilityTypeMappingService, UtilityTypeMappingService>();

        // Automatically scan for all mapping configurations. 
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddJobs();

        return services;
    }

    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var jobKey = new JobKey("UtilityBillPeriodStatusJob");
            
            q.AddJob<UtilityBillPeriodStatusJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("UtilityBillPeriodStatusJob-Trigger")
                // .WithCronSchedule("0 0 0 * * ?")
                .WithCronSchedule("0 * * ? * *")
            );
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        
        return services;
    }
}