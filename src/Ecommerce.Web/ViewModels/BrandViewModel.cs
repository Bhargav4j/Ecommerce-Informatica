using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Web.ViewModels;

public class BrandViewModel
{
    public int BrandId { get; set; }

    [Required(ErrorMessage = "Brand name is required")]
    [StringLength(50, ErrorMessage = "Brand name cannot exceed 50 characters")]
    [Display(Name = "Brand Name")]
    public string BrandName { get; set; } = string.Empty;
}
