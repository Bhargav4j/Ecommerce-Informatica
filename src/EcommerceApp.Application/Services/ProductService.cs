using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, IMapper mapper, ILogger<ProductService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products");
            throw;
        }
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            return product != null ? _mapper.Map<ProductDto>(product) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with id {ProductId}", id);
            throw;
        }
    }

    public async Task<ProductDto> CreateAsync(ProductCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var product = _mapper.Map<Product>(createDto);
            var created = await _repository.AddAsync(product, cancellationToken);
            return _mapper.Map<ProductDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            throw;
        }
    }

    public async Task UpdateAsync(int id, ProductUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Product with id {id} not found");
            }

            _mapper.Map(updateDto, existing);
            existing.Id = id;
            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with id {ProductId}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with id {ProductId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _repository.GetByCategoryAsync(categoryId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products by category {CategoryId}", categoryId);
            throw;
        }
    }
}
