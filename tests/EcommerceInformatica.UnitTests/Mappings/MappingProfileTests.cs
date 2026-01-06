using Xunit;
using AutoMapper;
using EcommerceInformatica.Application.Mappings;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Mappings;

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
        // Act & Assert
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Category_ToCategoryDto_ShouldMapCorrectly()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Electronics",
            Description = "Electronic devices",
            IsActive = true,
            CreatedDate = new DateTime(2024, 1, 1)
        };

        // Act
        var dto = _mapper.Map<CategoryDto>(category);

        // Assert
        Assert.Equal(category.Id, dto.Id);
        Assert.Equal(category.Name, dto.Name);
        Assert.Equal(category.Description, dto.Description);
        Assert.Equal(category.IsActive, dto.IsActive);
        Assert.Equal(category.CreatedDate, dto.CreatedDate);
    }

    [Fact]
    public void CategoryCreateDto_ToCategory_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new CategoryCreateDto
        {
            Name = "Electronics",
            Description = "Electronic devices",
            CreatedBy = "Admin"
        };

        // Act
        var category = _mapper.Map<Category>(dto);

        // Assert
        Assert.Equal(dto.Name, category.Name);
        Assert.Equal(dto.Description, category.Description);
        Assert.Equal(dto.CreatedBy, category.CreatedBy);
        Assert.True(category.IsActive);
        Assert.InRange(category.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    public void CategoryUpdateDto_ToCategory_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new CategoryUpdateDto
        {
            Name = "Electronics Updated",
            Description = "Updated description",
            IsActive = false,
            ModifiedBy = "Admin"
        };

        // Act
        var category = _mapper.Map<Category>(dto);

        // Assert
        Assert.Equal(dto.Name, category.Name);
        Assert.Equal(dto.Description, category.Description);
        Assert.Equal(dto.IsActive, category.IsActive);
        Assert.Equal(dto.ModifiedBy, category.ModifiedBy);
        Assert.InRange(category.ModifiedDate!.Value, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    public void Brand_ToBrandDto_ShouldMapCorrectly()
    {
        // Arrange
        var brand = new Brand
        {
            Id = 1,
            Name = "Samsung",
            Description = "Electronics brand",
            IsActive = true,
            CreatedDate = new DateTime(2024, 1, 1)
        };

        // Act
        var dto = _mapper.Map<BrandDto>(brand);

        // Assert
        Assert.Equal(brand.Id, dto.Id);
        Assert.Equal(brand.Name, dto.Name);
        Assert.Equal(brand.Description, dto.Description);
        Assert.Equal(brand.IsActive, dto.IsActive);
        Assert.Equal(brand.CreatedDate, dto.CreatedDate);
    }

    [Fact]
    public void BrandCreateDto_ToBrand_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new BrandCreateDto
        {
            Name = "Samsung",
            Description = "Electronics brand",
            CreatedBy = "Admin"
        };

        // Act
        var brand = _mapper.Map<Brand>(dto);

        // Assert
        Assert.Equal(dto.Name, brand.Name);
        Assert.Equal(dto.Description, brand.Description);
        Assert.Equal(dto.CreatedBy, brand.CreatedBy);
        Assert.True(brand.IsActive);
        Assert.InRange(brand.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }

    [Fact]
    public void Supplier_ToSupplierDto_ShouldMapCorrectly()
    {
        // Arrange
        var supplier = new Supplier
        {
            Id = 1,
            Name = "Tech Supplier Inc",
            Email = "contact@techsupplier.com",
            Phone = "123-456-7890",
            Address = "123 Main St",
            City = "New York",
            Province = "NY",
            IsActive = true,
            CreatedDate = new DateTime(2024, 1, 1)
        };

        // Act
        var dto = _mapper.Map<SupplierDto>(supplier);

        // Assert
        Assert.Equal(supplier.Id, dto.Id);
        Assert.Equal(supplier.Name, dto.Name);
        Assert.Equal(supplier.Email, dto.Email);
        Assert.Equal(supplier.Phone, dto.Phone);
        Assert.Equal(supplier.Address, dto.Address);
        Assert.Equal(supplier.City, dto.City);
        Assert.Equal(supplier.Province, dto.Province);
        Assert.Equal(supplier.IsActive, dto.IsActive);
        Assert.Equal(supplier.CreatedDate, dto.CreatedDate);
    }

    [Fact]
    public void SupplierCreateDto_ToSupplier_ShouldMapCorrectly()
    {
        // Arrange
        var dto = new SupplierCreateDto
        {
            Name = "Tech Supplier Inc",
            Email = "contact@techsupplier.com",
            Phone = "123-456-7890",
            Address = "123 Main St",
            City = "New York",
            Province = "NY",
            CreatedBy = "Admin"
        };

        // Act
        var supplier = _mapper.Map<Supplier>(dto);

        // Assert
        Assert.Equal(dto.Name, supplier.Name);
        Assert.Equal(dto.Email, supplier.Email);
        Assert.Equal(dto.Phone, supplier.Phone);
        Assert.Equal(dto.Address, supplier.Address);
        Assert.Equal(dto.City, supplier.City);
        Assert.Equal(dto.Province, supplier.Province);
        Assert.Equal(dto.CreatedBy, supplier.CreatedBy);
        Assert.True(supplier.IsActive);
        Assert.InRange(supplier.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
    }
}
