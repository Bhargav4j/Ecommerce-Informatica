using Xunit;
using AutoMapper;
using EcommerceApp.Application.Mappings;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Domain.Entities;

namespace EcommerceApp.Application.Mappings.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void MappingProfile_ConfigurationIsValid()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

        // Act & Assert
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_CategoryToDto_ShouldMapCorrectly()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Electronics", Description = "Test", IsActive = true };

        // Act
        var dto = _mapper.Map<CategoryDto>(category);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(1, dto.Id);
        Assert.Equal("Electronics", dto.Name);
        Assert.Equal("Test", dto.Description);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void Map_CategoryCreateDtoToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new CategoryCreateDto { Name = "Books", Description = "All books", CreatedBy = "Admin" };

        // Act
        var entity = _mapper.Map<Category>(createDto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal("Books", entity.Name);
        Assert.Equal("All books", entity.Description);
        Assert.Equal("Admin", entity.CreatedBy);
    }

    [Fact]
    public void Map_ProductToDto_ShouldMapCorrectly()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "Laptop",
            Price = 999.99m,
            Stock = 10,
            Category = new Category { Name = "Electronics" },
            Brand = new Brand { Name = "Dell" },
            Supplier = new Supplier { Name = "Tech Supplies" }
        };

        // Act
        var dto = _mapper.Map<ProductDto>(product);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(1, dto.Id);
        Assert.Equal("Laptop", dto.Name);
        Assert.Equal(999.99m, dto.Price);
        Assert.Equal(10, dto.Stock);
        Assert.Equal("Electronics", dto.CategoryName);
        Assert.Equal("Dell", dto.BrandName);
        Assert.Equal("Tech Supplies", dto.SupplierName);
    }

    [Fact]
    public void Map_ProductCreateDtoToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new ProductCreateDto
        {
            Name = "Mouse",
            Price = 29.99m,
            Stock = 50,
            CategoryId = 1,
            BrandId = 2,
            SupplierId = 3,
            CreatedBy = "Admin"
        };

        // Act
        var entity = _mapper.Map<Product>(createDto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal("Mouse", entity.Name);
        Assert.Equal(29.99m, entity.Price);
        Assert.Equal(50, entity.Stock);
        Assert.Equal(1, entity.CategoryId);
        Assert.Equal(2, entity.BrandId);
        Assert.Equal(3, entity.SupplierId);
        Assert.Equal("Admin", entity.CreatedBy);
    }

    [Fact]
    public void Map_PersonToDto_ShouldMapCorrectly()
    {
        // Arrange
        var person = new Person
        {
            Dni = "12345678",
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Phone = "555-1234",
            IsAdmin = true,
            IsActive = true
        };

        // Act
        var dto = _mapper.Map<PersonDto>(person);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal("12345678", dto.Dni);
        Assert.Equal("John", dto.FirstName);
        Assert.Equal("Doe", dto.LastName);
        Assert.Equal("john@example.com", dto.Email);
        Assert.Equal("555-1234", dto.Phone);
        Assert.True(dto.IsAdmin);
        Assert.True(dto.IsActive);
    }

    [Fact]
    public void Map_PersonCreateDtoToEntity_ShouldMapCorrectly()
    {
        // Arrange
        var createDto = new PersonCreateDto
        {
            Dni = "87654321",
            FirstName = "Jane",
            LastName = "Smith",
            Password = "password123",
            Email = "jane@example.com",
            Phone = "555-9876",
            IsAdmin = false,
            CreatedBy = "Admin"
        };

        // Act
        var entity = _mapper.Map<Person>(createDto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal("87654321", entity.Dni);
        Assert.Equal("Jane", entity.FirstName);
        Assert.Equal("Smith", entity.LastName);
        Assert.Equal("password123", entity.Password);
        Assert.Equal("jane@example.com", entity.Email);
        Assert.Equal("555-9876", entity.Phone);
        Assert.False(entity.IsAdmin);
        Assert.Equal("Admin", entity.CreatedBy);
    }
}
