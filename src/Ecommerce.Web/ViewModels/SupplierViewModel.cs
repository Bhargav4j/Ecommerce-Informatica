using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels;

public class SupplierViewModel
{
    public int SupplierId { get; set; }

    [Required(ErrorMessage = "Company name is required")]
    [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Contact name cannot exceed 50 characters")]
    [Display(Name = "Contact Name")]
    public string? ContactName { get; set; }

    [StringLength(50, ErrorMessage = "Contact title cannot exceed 50 characters")]
    [Display(Name = "Contact Title")]
    public string? ContactTitle { get; set; }

    [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
    [Display(Name = "Address")]
    public string? Address { get; set; }

    [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
    [Display(Name = "City")]
    public string? City { get; set; }

    [StringLength(50, ErrorMessage = "Region cannot exceed 50 characters")]
    [Display(Name = "Region")]
    public string? Region { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
    [Display(Name = "Country")]
    public string? Country { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format")]
    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [StringLength(20, ErrorMessage = "Fax number cannot exceed 20 characters")]
    [Display(Name = "Fax")]
    public string? Fax { get; set; }

    [Url(ErrorMessage = "Invalid URL format")]
    [StringLength(200, ErrorMessage = "Home page URL cannot exceed 200 characters")]
    [Display(Name = "Home Page")]
    public string? HomePage { get; set; }
}
