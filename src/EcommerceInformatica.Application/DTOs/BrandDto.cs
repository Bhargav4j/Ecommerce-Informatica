namespace EcommerceInformatica.Application.DTOs;

/// <summary>
/// Data Transfer Object for Brand
/// </summary>
public class BrandDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new Brand
/// </summary>
public class BrandCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

/// <summary>
/// DTO for updating an existing Brand
/// </summary>
public class BrandUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
