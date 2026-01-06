namespace EcommerceInformatica.Application.DTOs;

/// <summary>
/// Data Transfer Object for Person
/// </summary>
public class PersonDto
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public int? ProvinceId { get; set; }
    public string? ProvinceName { get; set; }
    public required string Username { get; set; }
    public required string Role { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for creating a new Person
/// </summary>
public class PersonCreateDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}

/// <summary>
/// DTO for updating an existing Person
/// </summary>
public class PersonUpdateDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }
    public bool IsActive { get; set; }
}
