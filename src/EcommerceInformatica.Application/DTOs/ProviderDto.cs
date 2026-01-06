namespace EcommerceInformatica.Application.DTOs;

/// <summary>
/// Data Transfer Object for Provider
/// </summary>
public class ProviderDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new Provider
/// </summary>
public class ProviderCreateDto
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
}

/// <summary>
/// DTO for updating an existing Provider
/// </summary>
public class ProviderUpdateDto
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}
