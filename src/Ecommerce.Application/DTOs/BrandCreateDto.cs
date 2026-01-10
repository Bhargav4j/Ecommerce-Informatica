using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class BrandCreateDto
{
    [Required(ErrorMessage = "Brand name is required")]
    [StringLength(40, ErrorMessage = "Brand name cannot exceed 40 characters")]
    public string BrandName { get; set; } = string.Empty;
}
