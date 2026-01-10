using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISupplierRepository supplierRepository,
        IBrandRepository brandRepository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _supplierRepository = supplierRepository ?? throw new ArgumentNullException(nameof(supplierRepository));
        _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting product with ID: {ProductId}", id);

            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found", id);
                return null;
            }

            return _mapper.Map<ProductDto>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product with ID: {ProductId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all products");

            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all products");
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting products for category ID: {CategoryId}", categoryId);

            var products = await _productRepository.GetByCategoryAsync(categoryId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products for category ID: {CategoryId}", categoryId);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetBySupplierAsync(int supplierId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting products for supplier ID: {SupplierId}", supplierId);

            var products = await _productRepository.GetBySupplierAsync(supplierId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products for supplier ID: {SupplierId}", supplierId);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetByBrandAsync(int brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting products for brand ID: {BrandId}", brandId);

            var products = await _productRepository.GetByBrandAsync(brandId, cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting products for brand ID: {BrandId}", brandId);
            throw;
        }
    }

    public async Task<ProductDto> CreateAsync(ProductCreateDto productCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new product: {ProductName}", productCreateDto.ProductName);

            // Validate related entities exist
            if (!await _categoryRepository.ExistsAsync(productCreateDto.CategoryId, cancellationToken))
            {
                throw new InvalidOperationException($"Category with ID {productCreateDto.CategoryId} does not exist");
            }

            if (!await _supplierRepository.ExistsAsync(productCreateDto.SupplierId, cancellationToken))
            {
                throw new InvalidOperationException($"Supplier with ID {productCreateDto.SupplierId} does not exist");
            }

            if (productCreateDto.BrandId.HasValue &&
                !await _brandRepository.ExistsAsync(productCreateDto.BrandId.Value, cancellationToken))
            {
                throw new InvalidOperationException($"Brand with ID {productCreateDto.BrandId} does not exist");
            }

            var product = _mapper.Map<Product>(productCreateDto);
            var createdProduct = await _productRepository.AddAsync(product, cancellationToken);

            _logger.LogInformation("Successfully created product with ID: {ProductId}", createdProduct.ProductId);

            return _mapper.Map<ProductDto>(createdProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {ProductName}", productCreateDto.ProductName);
            throw;
        }
    }

    public async Task<ProductDto?> UpdateAsync(ProductUpdateDto productUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", productUpdateDto.ProductId);

            var existingProduct = await _productRepository.GetByIdAsync(productUpdateDto.ProductId, cancellationToken);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found for update", productUpdateDto.ProductId);
                return null;
            }

            // Validate related entities exist
            if (!await _categoryRepository.ExistsAsync(productUpdateDto.CategoryId, cancellationToken))
            {
                throw new InvalidOperationException($"Category with ID {productUpdateDto.CategoryId} does not exist");
            }

            if (!await _supplierRepository.ExistsAsync(productUpdateDto.SupplierId, cancellationToken))
            {
                throw new InvalidOperationException($"Supplier with ID {productUpdateDto.SupplierId} does not exist");
            }

            if (productUpdateDto.BrandId.HasValue &&
                !await _brandRepository.ExistsAsync(productUpdateDto.BrandId.Value, cancellationToken))
            {
                throw new InvalidOperationException($"Brand with ID {productUpdateDto.BrandId} does not exist");
            }

            _mapper.Map(productUpdateDto, existingProduct);
            await _productRepository.UpdateAsync(existingProduct, cancellationToken);

            _logger.LogInformation("Successfully updated product with ID: {ProductId}", productUpdateDto.ProductId);

            return _mapper.Map<ProductDto>(existingProduct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID: {ProductId}", productUpdateDto.ProductId);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);

            if (!await _productRepository.ExistsAsync(id, cancellationToken))
            {
                _logger.LogWarning("Product with ID: {ProductId} not found for deletion", id);
                return false;
            }

            await _productRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted product with ID: {ProductId}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", id);
            throw;
        }
    }
}
