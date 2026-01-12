using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using Moq;
using Xunit;

namespace EcommerceInformatica.Tests.Application.Interfaces.Services;

/// <summary>
/// Tests to verify that all service interfaces follow the expected contract pattern.
/// These tests ensure interface consistency across all services.
/// </summary>
public class ServiceInterfaceContractTests
{
    #region IBrandService Tests

    [Fact]
    public async Task IBrandService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        var expectedBrands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "Brand1", Description = "Description1", IsActive = true },
            new BrandDto { Id = 2, Name = "Brand2", Description = "Description2", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task IBrandService_GetByIdAsync_ShouldReturnDto()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        var expectedBrand = new BrandDto { Id = 1, Name = "Brand1", Description = "Description1", IsActive = true };
        mockService.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrand);

        // Act
        var result = await mockService.Object.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task IBrandService_CreateAsync_ShouldReturnCreatedDto()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        var createDto = new BrandCreateDto { Name = "NewBrand", Description = "Description" };
        var expectedBrand = new BrandDto { Id = 1, Name = "NewBrand", Description = "Description", IsActive = true };
        mockService.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrand);

        // Act
        var result = await mockService.Object.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewBrand", result.Name);
    }

    [Fact]
    public async Task IBrandService_UpdateAsync_ShouldReturnUpdatedDto()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        var updateDto = new BrandUpdateDto { Name = "UpdatedBrand", Description = "Updated" };
        var expectedBrand = new BrandDto { Id = 1, Name = "UpdatedBrand", Description = "Updated", IsActive = true };
        mockService.Setup(s => s.UpdateAsync(1, updateDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrand);

        // Act
        var result = await mockService.Object.UpdateAsync(1, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdatedBrand", result.Name);
    }

    [Fact]
    public async Task IBrandService_DeleteAsync_ShouldReturnBoolean()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        mockService.Setup(s => s.DeleteAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await mockService.Object.DeleteAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IBrandService_SearchAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IBrandService>();
        var expectedBrands = new List<BrandDto>
        {
            new BrandDto { Id = 1, Name = "TestBrand", Description = "Description", IsActive = true }
        };
        mockService.Setup(s => s.SearchAsync("Test", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedBrands);

        // Act
        var result = await mockService.Object.SearchAsync("Test");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    #endregion

    #region ICategoryService Tests

    [Fact]
    public async Task ICategoryService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<ICategoryService>();
        var expectedCategories = new List<CategoryDto>
        {
            new CategoryDto { Id = 1, Name = "Category1", Description = "Description1", IsActive = true },
            new CategoryDto { Id = 2, Name = "Category2", Description = "Description2", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ICategoryService_CreateAsync_ShouldReturnCreatedDto()
    {
        // Arrange
        var mockService = new Mock<ICategoryService>();
        var createDto = new CategoryCreateDto { Name = "NewCategory", Description = "Description" };
        var expectedCategory = new CategoryDto { Id = 1, Name = "NewCategory", Description = "Description", IsActive = true };
        mockService.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCategory);

        // Act
        var result = await mockService.Object.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewCategory", result.Name);
    }

    #endregion

    #region ICityService Tests

    [Fact]
    public async Task ICityService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<ICityService>();
        var expectedCities = new List<CityDto>
        {
            new CityDto { Id = 1, Name = "City1", Description = "Description1", ProvinceId = 1, ProvinceName = "Province1", IsActive = true },
            new CityDto { Id = 2, Name = "City2", Description = "Description2", ProvinceId = 1, ProvinceName = "Province1", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCities);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ICityService_CreateAsync_ShouldReturnCreatedDto()
    {
        // Arrange
        var mockService = new Mock<ICityService>();
        var createDto = new CityCreateDto { Name = "NewCity", Description = "Description", ProvinceId = 1 };
        var expectedCity = new CityDto { Id = 1, Name = "NewCity", Description = "Description", ProvinceId = 1, ProvinceName = "Province1", IsActive = true };
        mockService.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedCity);

        // Act
        var result = await mockService.Object.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewCity", result.Name);
    }

    #endregion

    #region IInvoiceService Tests

    [Fact]
    public async Task IInvoiceService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IInvoiceService>();
        var expectedInvoices = new List<InvoiceDto>
        {
            new InvoiceDto { Id = 1, InvoiceDate = DateTime.Now, TotalAmount = 100, PersonId = 1, PersonName = "John Doe", PaymentMethodId = 1, PaymentMethodName = "Cash" },
            new InvoiceDto { Id = 2, InvoiceDate = DateTime.Now, TotalAmount = 200, PersonId = 1, PersonName = "John Doe", PaymentMethodId = 1, PaymentMethodName = "Cash" }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoices);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task IInvoiceService_CreateAsync_ShouldReturnCreatedDto()
    {
        // Arrange
        var mockService = new Mock<IInvoiceService>();
        var createDto = new InvoiceCreateDto { InvoiceDate = DateTime.Now, TotalAmount = 100, PersonId = 1, PaymentMethodId = 1 };
        var expectedInvoice = new InvoiceDto { Id = 1, InvoiceDate = DateTime.Now, TotalAmount = 100, PersonId = 1, PersonName = "John Doe", PaymentMethodId = 1, PaymentMethodName = "Cash" };
        mockService.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedInvoice);

        // Act
        var result = await mockService.Object.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(100, result.TotalAmount);
    }

    #endregion

    #region IInvoiceDetailService Tests

    [Fact]
    public async Task IInvoiceDetailService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IInvoiceDetailService>();
        var expectedDetails = new List<InvoiceDetailDto>
        {
            new InvoiceDetailDto { Id = 1, Quantity = 5, UnitPrice = 20, Subtotal = 100, InvoiceId = 1, ProductId = 1, ProductName = "Product1" },
            new InvoiceDetailDto { Id = 2, Quantity = 3, UnitPrice = 30, Subtotal = 90, InvoiceId = 1, ProductId = 2, ProductName = "Product2" }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDetails);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task IInvoiceDetailService_CreateAsync_ShouldReturnCreatedDto()
    {
        // Arrange
        var mockService = new Mock<IInvoiceDetailService>();
        var createDto = new InvoiceDetailCreateDto { Quantity = 5, UnitPrice = 20, Subtotal = 100, InvoiceId = 1, ProductId = 1 };
        var expectedDetail = new InvoiceDetailDto { Id = 1, Quantity = 5, UnitPrice = 20, Subtotal = 100, InvoiceId = 1, ProductId = 1, ProductName = "Product1" };
        mockService.Setup(s => s.CreateAsync(createDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDetail);

        // Act
        var result = await mockService.Object.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Quantity);
    }

    #endregion

    #region IPaymentMethodService Tests

    [Fact]
    public async Task IPaymentMethodService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IPaymentMethodService>();
        var expectedMethods = new List<PaymentMethodDto>
        {
            new PaymentMethodDto { Id = 1, Name = "Cash", Description = "Cash Payment", IsActive = true },
            new PaymentMethodDto { Id = 2, Name = "Card", Description = "Card Payment", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedMethods);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region IPersonService Tests

    [Fact]
    public async Task IPersonService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IPersonService>();
        var expectedPersons = new List<PersonDto>
        {
            new PersonDto { Id = 1, Dni = "12345678", FirstName = "John", LastName = "Doe", Email = "john@test.com", Phone = "123456", IsActive = true },
            new PersonDto { Id = 2, Dni = "87654321", FirstName = "Jane", LastName = "Smith", Email = "jane@test.com", Phone = "654321", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedPersons);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region IProductService Tests

    [Fact]
    public async Task IProductService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IProductService>();
        var expectedProducts = new List<ProductDto>
        {
            new ProductDto { Id = 1, Name = "Product1", Description = "Description1", UnitPrice = 100, Stock = 10, CategoryId = 1, BrandId = 1, SupplierId = 1, IsActive = true },
            new ProductDto { Id = 2, Name = "Product2", Description = "Description2", UnitPrice = 200, Stock = 20, CategoryId = 1, BrandId = 1, SupplierId = 1, IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region IProvinceService Tests

    [Fact]
    public async Task IProvinceService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<IProvinceService>();
        var expectedProvinces = new List<ProvinceDto>
        {
            new ProvinceDto { Id = 1, Name = "Province1", Description = "Description1", IsActive = true },
            new ProvinceDto { Id = 2, Name = "Province2", Description = "Description2", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProvinces);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region ISupplierService Tests

    [Fact]
    public async Task ISupplierService_GetAllAsync_ShouldReturnEnumerableOfDtos()
    {
        // Arrange
        var mockService = new Mock<ISupplierService>();
        var expectedSuppliers = new List<SupplierDto>
        {
            new SupplierDto { Id = 1, Name = "Supplier1", Description = "Description1", ContactName = "Contact1", ContactPhone = "123", ContactEmail = "s1@test.com", IsActive = true },
            new SupplierDto { Id = 2, Name = "Supplier2", Description = "Description2", ContactName = "Contact2", ContactPhone = "456", ContactEmail = "s2@test.com", IsActive = true }
        };
        mockService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSuppliers);

        // Act
        var result = await mockService.Object.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region Interface Contract Verification Tests

    [Fact]
    public void AllServiceInterfaces_ShouldHaveGetAllAsyncMethod()
    {
        // Verify that all service interfaces have GetAllAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("GetAllAsync");
            Assert.NotNull(method);
            Assert.True(method.ReturnType.IsGenericType);
        }
    }

    [Fact]
    public void AllServiceInterfaces_ShouldHaveGetByIdAsyncMethod()
    {
        // Verify that all service interfaces have GetByIdAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("GetByIdAsync");
            Assert.NotNull(method);
            Assert.True(method.ReturnType.IsGenericType);
        }
    }

    [Fact]
    public void AllServiceInterfaces_ShouldHaveCreateAsyncMethod()
    {
        // Verify that all service interfaces have CreateAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("CreateAsync");
            Assert.NotNull(method);
            Assert.True(method.ReturnType.IsGenericType);
        }
    }

    [Fact]
    public void AllServiceInterfaces_ShouldHaveUpdateAsyncMethod()
    {
        // Verify that all service interfaces have UpdateAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("UpdateAsync");
            Assert.NotNull(method);
            Assert.True(method.ReturnType.IsGenericType);
        }
    }

    [Fact]
    public void AllServiceInterfaces_ShouldHaveDeleteAsyncMethod()
    {
        // Verify that all service interfaces have DeleteAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("DeleteAsync");
            Assert.NotNull(method);
            Assert.Equal(typeof(Task<bool>), method.ReturnType);
        }
    }

    [Fact]
    public void AllServiceInterfaces_ShouldHaveSearchAsyncMethod()
    {
        // Verify that all service interfaces have SearchAsync method
        var serviceInterfaces = new[]
        {
            typeof(IBrandService),
            typeof(ICategoryService),
            typeof(ICityService),
            typeof(IInvoiceService),
            typeof(IInvoiceDetailService),
            typeof(IPaymentMethodService),
            typeof(IPersonService),
            typeof(IProductService),
            typeof(IProvinceService),
            typeof(ISupplierService)
        };

        foreach (var serviceInterface in serviceInterfaces)
        {
            var method = serviceInterface.GetMethod("SearchAsync");
            Assert.NotNull(method);
            Assert.True(method.ReturnType.IsGenericType);
        }
    }

    #endregion
}
