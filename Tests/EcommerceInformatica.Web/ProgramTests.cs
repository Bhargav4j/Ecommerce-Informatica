using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceInformatica.Web.Tests;

/// <summary>
/// Test class for Program configuration
/// </summary>
public class ProgramTests
{
    [Fact]
    public void WebApplicationBuilder_ShouldNotBeNull()
    {
        // Arrange
        var args = Array.Empty<string>();

        // Act
        var builder = WebApplication.CreateBuilder(args);

        // Assert
        Assert.NotNull(builder);
        Assert.NotNull(builder.Services);
    }

    [Fact]
    public void WebApplicationBuilder_Services_ShouldBeConfigurable()
    {
        // Arrange
        var args = Array.Empty<string>();
        var builder = WebApplication.CreateBuilder(args);

        // Act
        builder.Services.AddRazorPages();

        // Assert
        Assert.NotEmpty(builder.Services);
    }

    [Fact]
    public void WebApplicationBuilder_ShouldAllowServiceRegistration()
    {
        // Arrange
        var args = Array.Empty<string>();
        var builder = WebApplication.CreateBuilder(args);

        // Act
        builder.Services.AddSingleton<string>("test");
        var serviceProvider = builder.Services.BuildServiceProvider();
        var service = serviceProvider.GetService<string>();

        // Assert
        Assert.NotNull(service);
        Assert.Equal("test", service);
    }

    [Fact]
    public void WebApplicationBuilder_Configuration_ShouldNotBeNull()
    {
        // Arrange
        var args = Array.Empty<string>();
        var builder = WebApplication.CreateBuilder(args);

        // Act & Assert
        Assert.NotNull(builder.Configuration);
    }

    [Fact]
    public void WebApplicationBuilder_Environment_ShouldNotBeNull()
    {
        // Arrange
        var args = Array.Empty<string>();
        var builder = WebApplication.CreateBuilder(args);

        // Act & Assert
        Assert.NotNull(builder.Environment);
    }
}
