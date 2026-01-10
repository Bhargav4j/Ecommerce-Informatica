using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class PersonUpdateDto
{
    [Required(ErrorMessage = "Person ID is required")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(10, ErrorMessage = "First name cannot exceed 10 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters")]
    public string LastName { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }

    [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters")]
    public string? Address { get; set; }

    [StringLength(15, ErrorMessage = "City cannot exceed 15 characters")]
    public string? City { get; set; }

    [StringLength(15, ErrorMessage = "Region cannot exceed 15 characters")]
    public string? Region { get; set; }

    [StringLength(10, ErrorMessage = "Postal code cannot exceed 10 characters")]
    public string? PostalCode { get; set; }

    [StringLength(15, ErrorMessage = "Country cannot exceed 15 characters")]
    public string? Country { get; set; }

    [StringLength(24, ErrorMessage = "Home phone cannot exceed 24 characters")]
    public string? HomePhone { get; set; }

    [StringLength(4, ErrorMessage = "Extension cannot exceed 4 characters")]
    public string? Extension { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }
}
