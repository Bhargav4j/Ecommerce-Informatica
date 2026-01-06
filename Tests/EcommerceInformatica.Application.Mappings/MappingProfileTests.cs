using Xunit;
using AutoMapper;
using EcommerceInformatica.Application.Mappings;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Application.Mappings.Tests;

/// <summary>
/// Test class for MappingProfile
/// </summary>
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
    public void MappingProfile_Configuration_ShouldBeValid()
    {
        // Arrange
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        // Act & Assert
        configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void MappingProfile_Product_ToProductDto_ShouldMapCorrectly()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            ProviderId = 1,
            BrandId = 1,
            CategoryId = 1,
            Stock = 10,
            UnitPrice = 99.99m,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser",
            Provider = new Provider { Name = "Test Provider", CreatedBy = "System" },
            Brand = new Brand { Name = "Test Brand", CreatedBy = "System" },
            Category = new Category { Name = "Test Category", CreatedBy = "System" }
        };

        // Act
        var dto = _mapper.Map<ProductDto>(product);

        // Assert
        Assert.Equal(product.Id, dto.Id);
        Assert.Equal(product.Name, dto.Name);
        Assert.Equal(product.Description, dto.Description);
        Assert.Equal(product.ProviderId, dto.ProviderId);
        Assert.Equal("Test Provider", dto.ProviderName);
        Assert.Equal(product.BrandId, dto.BrandId);
        Assert.Equal("Test Brand", dto.BrandName);
        Assert.Equal(product.CategoryId, dto.CategoryId);
        Assert.Equal("Test Category", dto.CategoryName);
        Assert.Equal(product.Stock, dto.Stock);
        Assert.Equal(product.UnitPrice, dto.UnitPrice);
        Assert.Equal(product.IsActive, dto.IsActive);
    }

    [Fact]
    public void MappingProfile_ProductCreateDto_ToProduct_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new ProductCreateDto
        {
            Name = "New Product",
            Description = "New Description",
            ProviderId = 1,
            BrandId = 1,
            CategoryId = 1,
            Stock = 20,
            UnitPrice = 149.99m
        };

        // Act
        var product = _mapper.Map<Product>(dto);

        // Assert
        Assert.Equal(dto.Name, product.Name);
        Assert.Equal(dto.Description, product.Description);
        Assert.Equal(dto.ProviderId, product.ProviderId);
        Assert.Equal(dto.BrandId, product.BrandId);
        Assert.Equal(dto.CategoryId, product.CategoryId);
        Assert.Equal(dto.Stock, product.Stock);
        Assert.Equal(dto.UnitPrice, product.UnitPrice);
        Assert.True(product.IsActive);
        Assert.Equal("System", product.CreatedBy);
    }

    [Fact]
    public void MappingProfile_Category_ToCategoryDto_ShouldMapCorrectly()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Electronics",
            Description = "Electronic products",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        var dto = _mapper.Map<CategoryDto>(category);

        // Assert
        Assert.Equal(category.Id, dto.Id);
        Assert.Equal(category.Name, dto.Name);
        Assert.Equal(category.Description, dto.Description);
        Assert.Equal(category.IsActive, dto.IsActive);
    }

    [Fact]
    public void MappingProfile_Brand_ToBrandDto_ShouldMapCorrectly()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Apple",
            Description = "Apple Inc.",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        var dto = _mapper.Map<BrandDto>(brand);

        // Assert
        Assert.Equal(brand.Id, dto.Id);
        Assert.Equal(brand.Name, dto.Name);
        Assert.Equal(brand.Description, dto.Description);
        Assert.Equal(brand.IsActive, dto.IsActive);
    }

    [Fact]
    public void MappingProfile_Provider_ToProviderDto_ShouldMapCorrectly()
    {
        // Arrange
        var provider = new Provider
        {
            Id = 1,
            Name = "Tech Supplier",
            Email = "contact@tech.com",
            Phone = "123-456-7890",
            Address = "123 Tech St",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        var dto = _mapper.Map<ProviderDto>(provider);

        // Assert
        Assert.Equal(provider.Id, dto.Id);
        Assert.Equal(provider.Name, dto.Name);
        Assert.Equal(provider.Email, dto.Email);
        Assert.Equal(provider.Phone, dto.Phone);
        Assert.Equal(provider.Address, dto.Address);
        Assert.Equal(provider.IsActive, dto.IsActive);
    }

    [Fact]
    public void MappingProfile_Person_ToPersonDto_ShouldMapCorrectly()
    {
        // Arrange
        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Phone = "123-456-7890",
            Address = "123 Main St",
            CityId = 1,
            ProvinceId = 1,
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System",
            City = new City { Name = "Buenos Aires", CreatedBy = "System" },
            Province = new Province { Name = "Buenos Aires Province", CreatedBy = "System" }
        };

        // Act
        var dto = _mapper.Map<PersonDto>(person);

        // Assert
        Assert.Equal(person.Id, dto.Id);
        Assert.Equal(person.FirstName, dto.FirstName);
        Assert.Equal(person.LastName, dto.LastName);
        Assert.Equal(person.Email, dto.Email);
        Assert.Equal(person.Phone, dto.Phone);
        Assert.Equal(person.Address, dto.Address);
        Assert.Equal(person.CityId, dto.CityId);
        Assert.Equal("Buenos Aires", dto.CityName);
        Assert.Equal(person.ProvinceId, dto.ProvinceId);
        Assert.Equal("Buenos Aires Province", dto.ProvinceName);
        Assert.Equal(person.Username, dto.Username);
        Assert.Equal(person.Role, dto.Role);
        Assert.Equal(person.IsActive, dto.IsActive);
    }

    [Fact]
    public void MappingProfile_ProductUpdateDto_ToProduct_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new ProductUpdateDto
        {
            Name = "Updated Product",
            Description = "Updated Description",
            ProviderId = 2,
            BrandId = 2,
            CategoryId = 2,
            Stock = 30,
            UnitPrice = 199.99m,
            IsActive = false
        };

        // Act
        var product = _mapper.Map<Product>(dto);

        // Assert
        Assert.Equal(dto.Name, product.Name);
        Assert.Equal(dto.Description, product.Description);
        Assert.Equal(dto.ProviderId, product.ProviderId);
        Assert.Equal(dto.BrandId, product.BrandId);
        Assert.Equal(dto.CategoryId, product.CategoryId);
        Assert.Equal(dto.Stock, product.Stock);
        Assert.Equal(dto.UnitPrice, product.UnitPrice);
        Assert.Equal(dto.IsActive, product.IsActive);
        Assert.Equal("System", product.ModifiedBy);
    }
}
