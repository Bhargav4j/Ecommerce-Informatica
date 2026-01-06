using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for OrderDetail entity
/// </summary>
public class OrderDetailTests
{
    [Fact]
    public void OrderDetail_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            Id = 1,
            OrderId = 1,
            ProductId = 1,
            Quantity = 5,
            UnitPrice = 99.99m,
            Subtotal = 499.95m,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(1, orderDetail.Id);
        Assert.Equal(1, orderDetail.OrderId);
        Assert.Equal(1, orderDetail.ProductId);
        Assert.Equal(5, orderDetail.Quantity);
        Assert.Equal(99.99m, orderDetail.UnitPrice);
        Assert.Equal(499.95m, orderDetail.Subtotal);
        Assert.True(orderDetail.IsActive);
    }

    [Fact]
    public void OrderDetail_Quantity_ShouldAcceptPositiveValues()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            Quantity = 10,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(10, orderDetail.Quantity);
    }

    [Fact]
    public void OrderDetail_UnitPrice_ShouldAcceptDecimalValues()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            UnitPrice = 12345.67m,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(12345.67m, orderDetail.UnitPrice);
    }

    [Fact]
    public void OrderDetail_Subtotal_ShouldAcceptDecimalValues()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            Subtotal = 999.99m,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(999.99m, orderDetail.Subtotal);
    }

    [Fact]
    public void OrderDetail_NavigationProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            CreatedBy = "System"
        };

        // Assert
        Assert.Null(orderDetail.Order);
        Assert.Null(orderDetail.Product);
    }

    [Fact]
    public void OrderDetail_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            CreatedBy = "System",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(orderDetail.ModifiedDate);
    }

    [Fact]
    public void OrderDetail_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            CreatedBy = "System"
        };

        // Assert
        Assert.False(orderDetail.IsActive);
    }

    [Fact]
    public void OrderDetail_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var orderDetail = new OrderDetail
            {
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void OrderDetail_ModifiedBy_ShouldBeNullable()
    {
        // Arrange & Act
        var orderDetail = new OrderDetail
        {
            CreatedBy = "System",
            ModifiedBy = null
        };

        // Assert
        Assert.Null(orderDetail.ModifiedBy);
    }
}
