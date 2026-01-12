using Microsoft.EntityFrameworkCore;
using Serilog;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Application.Extensions;
using EcommerceInformatica.Infrastructure.Extensions;

// WebApplication.CreateBuilder(args) automatically includes environment variables in .NET 8.0
// Environment variables can override appsettings.json values at runtime
// Supported environment variables:
// - ASPNETCORE_ENVIRONMENT: Set environment (Development, Staging, Production)
// - ConnectionStrings__DefaultConnection: Override database connection string
// - DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD: Override individual DB settings
// - Logging__LogLevel__Default: Override logging level
var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
// Logs are written to console only for container compatibility
// Container platforms (Docker, Kubernetes) will capture console output for log aggregation
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorPages();

// Add health checks for container orchestration
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null);
            npgsqlOptions.MigrationsHistoryTable("__ef_migrations_history", "public");
        })
        .UseSnakeCaseNamingConvention()
        .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());

    // Configure legacy timestamp behavior for PostgreSQL
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
});

// Register Application services
builder.Services.AddApplicationServices();

// Register Infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Map health check endpoints for container orchestration (Kubernetes, Docker, ECS)
app.MapHealthChecks("/health");

try
{
    Log.Information("Starting web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
