using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

/// <summary>
/// Service implementation for Product operations
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all products");
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products");
            throw;
        }
    }

    public async Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving product with id {Id}", id);
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with id {Id}", id);
            throw;
        }
    }

    public async Task<object> CreateAsync(object productCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var dto = (ProductCreateDto)productCreateDto;
            _logger.LogInformation("Creating new product: {Name}", dto.Name);
            var product = _mapper.Map<Product>(dto);
            var created = await _productRepository.AddAsync(product, cancellationToken);
            return _mapper.Map<ProductDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            throw;
        }
    }

    public async Task UpdateAsync(int id, object productUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var dto = (ProductUpdateDto)productUpdateDto;
            _logger.LogInformation("Updating product with id {Id}", id);
            var existingProduct = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with id {id} not found");
            }
            _mapper.Map(dto, existingProduct);
            await _productRepository.UpdateAsync(existingProduct, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting product with id {Id}", id);
            await _productRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
            var products = await _productRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products");
            throw;
        }
    }

    public async Task<IEnumerable<object>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving products for category {CategoryId}", categoryId);
            var products = await _productRepository.GetByCategoryAsync(categoryId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products for category {CategoryId}", categoryId);
            throw;
        }
    }
}
