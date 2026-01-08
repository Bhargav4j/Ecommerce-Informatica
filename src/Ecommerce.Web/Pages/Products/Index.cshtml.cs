using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;

namespace Ecommerce.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<IndexModel> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public string? SearchTerm { get; set; }

    public async Task<IActionResult> OnGetAsync(string? searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            SearchTerm = searchTerm;
            var products = string.IsNullOrWhiteSpace(searchTerm)
                ? await _productRepository.GetAllAsync(cancellationToken)
                : await _productRepository.SearchAsync(searchTerm, cancellationToken);

            Products = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products");
            return RedirectToPage("/Error");
        }
    }
}
