using Xunit;
using EcommerceApp.Web.Pages;
using Microsoft.Extensions.Logging;
using Moq;

namespace EcommerceApp.Web.Pages.Tests;

public class IndexModelTests
{
    private readonly Mock<ILogger<IndexModel>> _loggerMock;
    private readonly IndexModel _model;

    public IndexModelTests()
    {
        _loggerMock = new Mock<ILogger<IndexModel>>();
        _model = new IndexModel(_loggerMock.Object);
    }

    [Fact]
    public void IndexModel_Constructor_ShouldInitialize()
    {
        // Arrange & Act
        var model = new IndexModel(_loggerMock.Object);

        // Assert
        Assert.NotNull(model);
    }

    [Fact]
    public void OnGet_ShouldExecuteWithoutErrors()
    {
        // Arrange
        var model = new IndexModel(_loggerMock.Object);

        // Act
        model.OnGet();

        // Assert - No exception thrown
        Assert.NotNull(model);
    }

    [Fact]
    public void OnGet_ShouldLogInformation()
    {
        // Arrange
        var model = new IndexModel(_loggerMock.Object);

        // Act
        model.OnGet();

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
            Times.Once);
    }
}
