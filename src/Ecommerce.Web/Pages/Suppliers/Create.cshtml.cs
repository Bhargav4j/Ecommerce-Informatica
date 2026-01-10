using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Web.Pages.Suppliers;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ISupplierService _supplierService;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(ISupplierService supplierService, ILogger<CreateModel> logger)
    {
        _supplierService = supplierService;
        _logger = logger;
    }

    [BindProperty]
    public SupplierViewModel Supplier { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var createDto = new SupplierCreateDto
            {
                CompanyName = Supplier.CompanyName,
                ContactName = Supplier.ContactName,
                ContactTitle = Supplier.ContactTitle,
                Address = Supplier.Address,
                City = Supplier.City,
                Region = Supplier.Region,
                PostalCode = Supplier.PostalCode,
                Country = Supplier.Country,
                Phone = Supplier.Phone,
                Fax = Supplier.Fax,
                HomePage = Supplier.HomePage
            };

            var createdSupplier = await _supplierService.CreateAsync(createDto);
            _logger.LogInformation("Supplier created successfully: {CompanyName} (ID: {SupplierId})",
                createdSupplier.CompanyName, createdSupplier.SupplierId);

            TempData["SuccessMessage"] = $"Supplier '{createdSupplier.CompanyName}' created successfully!";
            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating supplier: {CompanyName}", Supplier.CompanyName);
            ModelState.AddModelError(string.Empty, "An error occurred while creating the supplier. Please try again.");
            return Page();
        }
    }
}
