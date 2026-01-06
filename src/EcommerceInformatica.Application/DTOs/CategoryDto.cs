namespace EcommerceInformatica.Application.DTOs;

/// <summary>
/// Data Transfer Object for Category
/// </summary>
public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new Category
/// </summary>
public class CategoryCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

/// <summary>
/// DTO for updating an existing Category
/// </summary>
public class CategoryUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
