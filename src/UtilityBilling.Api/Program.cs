using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using UtilityBilling.Api;
using UtilityBilling.Application;
using UtilityBilling.Infrastructure;
using UtilityBilling.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
    });

var app = builder.Build();

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

    context.Database.Migrate(); // Applies migrations

    // You can seed data here if needed
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder =>
{
    builder
        .WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// TODO: This ugly part will be removed when this issue is fixed: https://github.com/dotnet/aspnetcore/issues/51888
app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
