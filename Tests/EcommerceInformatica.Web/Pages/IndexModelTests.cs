using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using EcommerceInformatica.Web.Pages;

namespace EcommerceInformatica.Web.Tests.Pages;

public class IndexModelTests
{
    private readonly Mock<ILogger<IndexModel>> _mockLogger;
    private readonly IndexModel _pageModel;

    public IndexModelTests()
    {
        _mockLogger = new Mock<ILogger<IndexModel>>();
        _pageModel = new IndexModel(_mockLogger.Object);
    }

    [Fact]
    public void OnGet_ExecutesSuccessfully()
    {
        // Act
        _pageModel.OnGet();

        // Assert - Method completes without throwing exceptions
        Assert.NotNull(_pageModel);
    }

    [Fact]
    public void OnGet_LogsInformation()
    {
        // Act
        _pageModel.OnGet();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Home page accessed")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void Constructor_InitializesWithLogger()
    {
        // Act
        var pageModel = new IndexModel(_mockLogger.Object);

        // Assert
        Assert.NotNull(pageModel);
    }

    [Fact]
    public void OnGet_DoesNotReturnValue()
    {
        // Act & Assert - OnGet is void method
        _pageModel.OnGet();

        // If we reach here without exception, the test passes
        Assert.True(true);
    }

    [Fact]
    public void OnGet_CanBeCalledMultipleTimes()
    {
        // Act
        _pageModel.OnGet();
        _pageModel.OnGet();
        _pageModel.OnGet();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Home page accessed")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Exactly(3));
    }
}
