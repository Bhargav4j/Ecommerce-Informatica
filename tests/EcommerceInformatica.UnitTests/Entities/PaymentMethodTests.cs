using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class PaymentMethodTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.Equal(0, paymentMethod.Id);
        Assert.Equal(string.Empty, paymentMethod.Name);
        Assert.Null(paymentMethod.Description);
        Assert.True(paymentMethod.IsActive);
        Assert.InRange(paymentMethod.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(paymentMethod.ModifiedDate);
        Assert.Equal("System", paymentMethod.CreatedBy);
        Assert.Null(paymentMethod.ModifiedBy);
        Assert.NotNull(paymentMethod.Invoices);
        Assert.Empty(paymentMethod.Invoices);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Id = 100;

        // Assert
        Assert.Equal(100, paymentMethod.Id);
    }

    [Fact]
    public void Name_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Name = "Credit Card";

        // Assert
        Assert.Equal("Credit Card", paymentMethod.Name);
    }

    [Fact]
    public void Description_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Description = "Payment via credit card";

        // Assert
        Assert.Equal("Payment via credit card", paymentMethod.Description);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.IsActive = false;

        // Assert
        Assert.False(paymentMethod.IsActive);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var date = new DateTime(2024, 1, 1);

        // Act
        paymentMethod.CreatedDate = date;

        // Assert
        Assert.Equal(date, paymentMethod.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var date = new DateTime(2024, 1, 2);

        // Act
        paymentMethod.ModifiedDate = date;

        // Assert
        Assert.Equal(date, paymentMethod.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", paymentMethod.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", paymentMethod.ModifiedBy);
    }

    [Fact]
    public void Invoices_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.NotNull(paymentMethod.Invoices);
        Assert.Empty(paymentMethod.Invoices);
        Assert.IsAssignableFrom<ICollection<Invoice>>(paymentMethod.Invoices);
    }

    [Fact]
    public void PaymentMethod_ShouldAllowInvoicesToBeAdded()
    {
        // Arrange
        var paymentMethod = new PaymentMethod { Id = 1, Name = "Credit Card" };
        var invoice = new Invoice { Id = 1, PaymentMethodId = 1 };

        // Act
        paymentMethod.Invoices.Add(invoice);

        // Assert
        Assert.Single(paymentMethod.Invoices);
        Assert.Contains(invoice, paymentMethod.Invoices);
    }
}
