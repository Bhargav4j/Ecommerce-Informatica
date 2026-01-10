using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(15, ErrorMessage = "Category name cannot exceed 15 characters")]
    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }
}
