using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels;

public class PersonViewModel
{
    public int PersonId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Full Name")]
    public string FullName => $"{FirstName} {LastName}";

    [DataType(DataType.Date)]
    [Display(Name = "Birth Date")]
    public DateTime? BirthDate { get; set; }

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
    [Display(Name = "Home Phone")]
    public string? HomePhone { get; set; }

    [StringLength(10, ErrorMessage = "Extension cannot exceed 10 characters")]
    [Display(Name = "Extension")]
    public string? Extension { get; set; }

    [Display(Name = "Photo")]
    public byte[]? Photo { get; set; }

    [Display(Name = "Upload Photo")]
    public IFormFile? PhotoFile { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    [Display(Name = "Notes")]
    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }
}
