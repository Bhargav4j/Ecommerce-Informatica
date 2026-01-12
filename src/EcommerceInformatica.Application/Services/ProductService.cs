using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository repository,
        IMapper mapper,
        ILogger<ProductService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all products");
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} products", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all products");
            throw;
        }
    }

    public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting product with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Product with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<ProductDto>(entity);
            _logger.LogInformation("Successfully retrieved product with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<ProductDto> CreateAsync(ProductCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new product: {Name}", createDto.Name);

            var entity = _mapper.Map<Product>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<ProductDto>(createdEntity);

            _logger.LogInformation("Successfully created product with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating product: {Name}", createDto?.Name);
            throw;
        }
    }

    public async Task<ProductDto> UpdateAsync(int id, ProductUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating product with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Product with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<ProductDto>(updatedEntity);

            _logger.LogInformation("Successfully updated product with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Product with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted product with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(entities);
            _logger.LogInformation("Successfully found {Count} products matching search term: {SearchTerm}", dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching products with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
