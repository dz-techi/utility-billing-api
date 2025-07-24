using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using UtilityBilling.Api;
using UtilityBilling.Api.Extensions;
using UtilityBilling.Api.Services.Interfaces;
using UtilityBilling.Application;
using UtilityBilling.Infrastructure;
using UtilityBilling.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization to use string enums
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

// Debug configuration loading
var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"Current environment: {environment}");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpoints(typeof(Program).Assembly);

builder.Services
    .AddApi(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Logging.AddOpenTelemetry(options =>
{
    options.AddConsoleExporter()
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("UtilityBilling.Api"));

    options.AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://localhost:18889");
    });
});

var app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply migrations and seed data (if needed)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dataSeedingService = services.GetRequiredService<IDataSeedingService>();
    
    try
    {
        logger.LogInformation("Starting database migration...");

        // Apply migrations
        await context.Database.MigrateAsync();

        logger.LogInformation("Database migration completed successfully.");

        if (app.Environment.IsDevelopment())
        {
            await dataSeedingService.SeedTestingData();
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database: {Message}", ex.Message);

        // In development, you might want to throw to see the full error
        if (app.Environment.IsDevelopment())
        {
            throw;
        }
    }
}

app.UseAuthentication();

app.UseCors(builder =>
{
    builder
        .WithOrigins(
            "http://localhost:3000",  // React default dev server
            "http://localhost:5173",  // Vite dev server
            "http://localhost:3001"   // Alternative React port
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseAuthorization();


// TODO: This ugly part will be removed when this issue is fixed: https://github.com/dotnet/aspnetcore/issues/51888
app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

// Map endpoints using Vertical Slice Architecture
// app.MapGetProductEndpoint();
// app.MapAddProductEndpoint();
//
// app.MapGetUtilityBillPeriodsEndpoint();
// app.MapAddUtilityBillPeriodEndpoint();
// app.MapGetUtilityBillPeriodByIdEndpoint();
// app.MapRemoveUtilityBillPeriodEndpoint();
// app.MapAddUtilityBillEndpoint();
// app.MapUpdateUtilityBillEndpoint();
// app.MapRemoveUtilityBillEndpoint();
// app.MapGetPropertiesForSelectEndpoint();

app.Run();
