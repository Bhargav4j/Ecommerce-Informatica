namespace EcommerceInformatica.Domain.Entities;

/// <summary>
/// Person entity for customers and users
/// </summary>
public class Person
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public int? ProvinceId { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public City? City { get; set; }
    public Province? Province { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
