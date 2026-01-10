using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class SupplierUpdateDto
{
    [Required(ErrorMessage = "Supplier ID is required")]
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    [StringLength(40, ErrorMessage = "Company name cannot exceed 40 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Contact name cannot exceed 30 characters")]
    public string? ContactName { get; set; }

    [StringLength(30, ErrorMessage = "Contact title cannot exceed 30 characters")]
    public string? ContactTitle { get; set; }

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

    [StringLength(24, ErrorMessage = "Phone cannot exceed 24 characters")]
    public string? Phone { get; set; }

    [StringLength(24, ErrorMessage = "Fax cannot exceed 24 characters")]
    public string? Fax { get; set; }

    public string? HomePage { get; set; }
}
