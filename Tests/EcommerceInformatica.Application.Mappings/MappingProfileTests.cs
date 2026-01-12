using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Mappings;
using EcommerceInformatica.Domain.Entities;
using Xunit;

namespace EcommerceInformatica.Tests.Application.Mappings;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void MappingProfile_ShouldHaveValidConfiguration()
    {
        // Assert
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    #region Product Mappings

    [Fact]
    public void Map_ProductToProductDto_ShouldMapCorrectly()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 99.99m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1,
            Category = new Category { Id = 1, Name = "Test Category", Description = "Test" },
            Brand = new Brand { Id = 1, Name = "Test Brand", Description = "Test" },
            Supplier = new Supplier { Id = 1, Name = "Test Supplier", Description = "Test", ContactName = "John", ContactPhone = "123", ContactEmail = "test@test.com" }
        };

        // Act
        var result = _mapper.Map<ProductDto>(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product.Id, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.UnitPrice, result.UnitPrice);
        Assert.Equal(product.Stock, result.Stock);
        Assert.Equal("Test Category", result.CategoryName);
        Assert.Equal("Test Brand", result.BrandName);
        Assert.Equal("Test Supplier", result.SupplierName);
    }

    [Fact]
    public void Map_ProductCreateDtoToProduct_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new ProductCreateDto
        {
            Name = "Test Product",
            Description = "Test Description",
            UnitPrice = 99.99m,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1
        };

        // Act
        var result = _mapper.Map<Product>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Description, result.Description);
        Assert.Equal(createDto.UnitPrice, result.UnitPrice);
        Assert.Equal(createDto.Stock, result.Stock);
        Assert.Equal(createDto.CategoryId, result.CategoryId);
        Assert.Equal(createDto.BrandId, result.BrandId);
        Assert.Equal(createDto.SupplierId, result.SupplierId);
    }

    [Fact]
    public void Map_ProductUpdateDtoToProduct_ShouldMapCorrectly()
    {
        // Arrange
        var updateDto = new ProductUpdateDto
        {
            Name = "Updated Product",
            Description = "Updated Description",
            UnitPrice = 199.99m,
            Stock = 20,
            CategoryId = 2,
            BrandId = 2,
            SupplierId = 2
        };

        // Act
        var result = _mapper.Map<Product>(updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.Name, result.Name);
        Assert.Equal(updateDto.Description, result.Description);
        Assert.Equal(updateDto.UnitPrice, result.UnitPrice);
        Assert.Equal(updateDto.Stock, result.Stock);
    }

    #endregion

    #region Category Mappings

    [Fact]
    public void Map_CategoryToCategoryDto_ShouldMapCorrectly()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Test Category",
            Description = "Test Description",
            IsActive = true
        };

        // Act
        var result = _mapper.Map<CategoryDto>(category);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal(category.Name, result.Name);
        Assert.Equal(category.Description, result.Description);
        Assert.Equal(category.IsActive, result.IsActive);
    }

    [Fact]
    public void Map_CategoryCreateDtoToCategory_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new CategoryCreateDto
        {
            Name = "Test Category",
            Description = "Test Description"
        };

        // Act
        var result = _mapper.Map<Category>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Description, result.Description);
    }

    #endregion

    #region Brand Mappings

    [Fact]
    public void Map_BrandToBrandDto_ShouldMapCorrectly()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Test Brand",
            Description = "Test Description",
            IsActive = true
        };

        // Act
        var result = _mapper.Map<BrandDto>(brand);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(brand.Id, result.Id);
        Assert.Equal(brand.Name, result.Name);
        Assert.Equal(brand.Description, result.Description);
        Assert.Equal(brand.IsActive, result.IsActive);
    }

    [Fact]
    public void Map_BrandCreateDtoToBrand_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new BrandCreateDto
        {
            Name = "Test Brand",
            Description = "Test Description"
        };

        // Act
        var result = _mapper.Map<Brand>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal(createDto.Description, result.Description);
    }

    #endregion

    #region Supplier Mappings

    [Fact]
    public void Map_SupplierToSupplierDto_ShouldMapCorrectly()
    {
        // Arrange
        var supplier = new Supplier
        {
            Id = 1,
            Name = "Test Supplier",
            Description = "Test Description",
            ContactName = "John Doe",
            ContactPhone = "1234567890",
            ContactEmail = "test@test.com",
            IsActive = true
        };

        // Act
        var result = _mapper.Map<SupplierDto>(supplier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(supplier.Id, result.Id);
        Assert.Equal(supplier.Name, result.Name);
        Assert.Equal(supplier.Description, result.Description);
        Assert.Equal(supplier.ContactName, result.ContactName);
        Assert.Equal(supplier.ContactPhone, result.ContactPhone);
        Assert.Equal(supplier.ContactEmail, result.ContactEmail);
        Assert.Equal(supplier.IsActive, result.IsActive);
    }

    #endregion

    #region Person Mappings

    [Fact]
    public void Map_PersonToPersonDto_ShouldMapCorrectly()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Phone = "1234567890",
            IsAdmin = false,
            IsActive = true
        };

        // Act
        var result = _mapper.Map<PersonDto>(person);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(person.Id, result.Id);
        Assert.Equal(person.Dni, result.Dni);
        Assert.Equal(person.FirstName, result.FirstName);
        Assert.Equal(person.LastName, result.LastName);
        Assert.Equal(person.Email, result.Email);
        Assert.Equal(person.Phone, result.Phone);
    }

    [Fact]
    public void Map_PersonCreateDtoToPerson_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new PersonCreateDto
        {
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test.com",
            Phone = "1234567890",
            Password = "hashedpassword"
        };

        // Act
        var result = _mapper.Map<Person>(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Dni, result.Dni);
        Assert.Equal(createDto.FirstName, result.FirstName);
        Assert.Equal(createDto.LastName, result.LastName);
        Assert.Equal(createDto.Email, result.Email);
        Assert.Equal(createDto.Phone, result.Phone);
    }

    #endregion

    #region Invoice Mappings

    [Fact]
    public void Map_InvoiceToInvoiceDto_ShouldMapCorrectly()
    {
        // Arrange
        var invoice = new Invoice
        {
            Id = 1,
            InvoiceDate = DateTime.Now,
            TotalAmount = 100.00m,
            PersonId = 1,
            PaymentMethodId = 1,
            Person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Dni = "123", Email = "test@test.com", Phone = "123", PasswordHash = "hash" },
            PaymentMethod = new PaymentMethod { Id = 1, Name = "Cash", Description = "Cash Payment" }
        };

        // Act
        var result = _mapper.Map<InvoiceDto>(invoice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(invoice.Id, result.Id);
        Assert.Equal(invoice.InvoiceDate, result.InvoiceDate);
        Assert.Equal(invoice.TotalAmount, result.TotalAmount);
        Assert.Equal("John Doe", result.PersonName);
        Assert.Equal("Cash", result.PaymentMethodName);
    }

    #endregion

    #region InvoiceDetail Mappings

    [Fact]
    public void Map_InvoiceDetailToInvoiceDetailDto_ShouldMapCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail
        {
            Id = 1,
            Quantity = 5,
            UnitPrice = 20.00m,
            Subtotal = 100.00m,
            InvoiceId = 1,
            ProductId = 1,
            Product = new Product { Id = 1, Name = "Test Product", Description = "Test", UnitPrice = 20m, Stock = 10, CategoryId = 1, BrandId = 1, SupplierId = 1 }
        };

        // Act
        var result = _mapper.Map<InvoiceDetailDto>(invoiceDetail);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(invoiceDetail.Id, result.Id);
        Assert.Equal(invoiceDetail.Quantity, result.Quantity);
        Assert.Equal(invoiceDetail.UnitPrice, result.UnitPrice);
        Assert.Equal(invoiceDetail.Subtotal, result.Subtotal);
        Assert.Equal("Test Product", result.ProductName);
    }

    #endregion

    #region PaymentMethod Mappings

    [Fact]
    public void Map_PaymentMethodToPaymentMethodDto_ShouldMapCorrectly()
    {
        // Arrange
        var paymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Credit Card Payment",
            IsActive = true
        };

        // Act
        var result = _mapper.Map<PaymentMethodDto>(paymentMethod);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(paymentMethod.Id, result.Id);
        Assert.Equal(paymentMethod.Name, result.Name);
        Assert.Equal(paymentMethod.Description, result.Description);
        Assert.Equal(paymentMethod.IsActive, result.IsActive);
    }

    #endregion

    #region Province Mappings

    [Fact]
    public void Map_ProvinceToProvinceDto_ShouldMapCorrectly()
    {
        // Arrange
        var province = new Province
        {
            Id = 1,
            Name = "Test Province",
            Description = "Test Description",
            IsActive = true
        };

        // Act
        var result = _mapper.Map<ProvinceDto>(province);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(province.Id, result.Id);
        Assert.Equal(province.Name, result.Name);
        Assert.Equal(province.Description, result.Description);
        Assert.Equal(province.IsActive, result.IsActive);
    }

    #endregion

    #region City Mappings

    [Fact]
    public void Map_CityToCityDto_ShouldMapCorrectly()
    {
        // Arrange
        var city = new City
        {
            Id = 1,
            Name = "Test City",
            Description = "Test Description",
            ProvinceId = 1,
            Province = new Province { Id = 1, Name = "Test Province", Description = "Test" },
            IsActive = true
        };

        // Act
        var result = _mapper.Map<CityDto>(city);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(city.Id, result.Id);
        Assert.Equal(city.Name, result.Name);
        Assert.Equal(city.Description, result.Description);
        Assert.Equal("Test Province", result.ProvinceName);
        Assert.Equal(city.IsActive, result.IsActive);
    }

    #endregion
}
