using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class BrandUpdateDto
{
    [Required(ErrorMessage = "Brand ID is required")]
    public int BrandId { get; set; }

    [Required(ErrorMessage = "Brand name is required")]
    [StringLength(40, ErrorMessage = "Brand name cannot exceed 40 characters")]
    public string BrandName { get; set; } = string.Empty;
}
