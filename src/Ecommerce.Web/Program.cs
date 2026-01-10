using Ecommerce.Application.Extensions;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/ecommerce-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting Ecommerce Web Application");

    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddRazorPages(options =>
    {
        options.Conventions.AuthorizePage("/Products/Index");
        options.Conventions.AuthorizePage("/Products/Create");
        options.Conventions.AuthorizePage("/Products/Edit");
        options.Conventions.AuthorizePage("/Products/Details");
        options.Conventions.AuthorizePage("/Products/Delete");
        options.Conventions.AuthorizePage("/Categories/Index");
        options.Conventions.AuthorizePage("/Categories/Create");
        options.Conventions.AuthorizePage("/Categories/Edit");
        options.Conventions.AuthorizePage("/Categories/Details");
        options.Conventions.AuthorizePage("/Categories/Delete");
        options.Conventions.AuthorizePage("/Brands/Index");
        options.Conventions.AuthorizePage("/Brands/Create");
        options.Conventions.AuthorizePage("/Brands/Edit");
        options.Conventions.AuthorizePage("/Brands/Details");
        options.Conventions.AuthorizePage("/Brands/Delete");
        options.Conventions.AuthorizePage("/Suppliers/Index");
        options.Conventions.AuthorizePage("/Suppliers/Create");
        options.Conventions.AuthorizePage("/Suppliers/Edit");
        options.Conventions.AuthorizePage("/Suppliers/Details");
        options.Conventions.AuthorizePage("/Suppliers/Delete");
        options.Conventions.AuthorizePage("/Persons/Index");
        options.Conventions.AuthorizePage("/Persons/Create");
        options.Conventions.AuthorizePage("/Persons/Edit");
        options.Conventions.AuthorizePage("/Persons/Details");
        options.Conventions.AuthorizePage("/Persons/Delete");
    });

    // Configure Authentication
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login";
            options.LogoutPath = "/Logout";
            options.AccessDeniedPath = "/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
        });

    builder.Services.AddAuthorization();

    // Configure DbContext
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.AddDbContext<EcommerceDbContext>(options =>
    {
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            sqlOptions.CommandTimeout(60);
        });

        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        }
    });

    // Add Infrastructure and Application services
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    // Add Session support
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Add HTTP Context Accessor
    builder.Services.AddHttpContextAccessor();

    // Add AntiForgery
    builder.Services.AddAntiforgery(options =>
    {
        options.HeaderName = "X-CSRF-TOKEN";
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.UseSerilogRequestLogging();

    app.MapRazorPages();

    // Default redirect to Login if not authenticated
    app.MapGet("/", context =>
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            context.Response.Redirect("/Login");
        }
        else
        {
            context.Response.Redirect("/Index");
        }
        return Task.CompletedTask;
    });

    // Ensure database is created (for development)
    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

        try
        {
            // Check if database can be connected
            if (dbContext.Database.CanConnect())
            {
                Log.Information("Database connection successful");
            }
            else
            {
                Log.Warning("Cannot connect to database. Please ensure SQL Server is running and connection string is correct.");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while checking database connection");
        }
    }

    app.Run();

    Log.Information("Ecommerce Web Application stopped successfully");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Ecommerce Web Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
