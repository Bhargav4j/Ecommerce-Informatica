using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper,
        ILogger<CategoryService> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all categories");
            var entities = await _repository.GetAllAsync(cancellationToken);
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(entities);
            _logger.LogInformation("Successfully retrieved {Count} categories", dtos.Count());
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all categories");
            throw;
        }
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting category with ID: {Id}", id);
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Category with ID: {Id} not found", id);
                return null;
            }

            var dto = _mapper.Map<CategoryDto>(entity);
            _logger.LogInformation("Successfully retrieved category with ID: {Id}", id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting category with ID: {Id}", id);
            throw;
        }
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            _logger.LogInformation("Creating new category: {Name}", createDto.Name);

            var entity = _mapper.Map<Category>(createDto);
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = "System"; // TODO: Get from current user context
            entity.IsActive = true;

            var createdEntity = await _repository.AddAsync(entity, cancellationToken);
            var dto = _mapper.Map<CategoryDto>(createdEntity);

            _logger.LogInformation("Successfully created category with ID: {Id}", createdEntity.Id);
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating category: {Name}", createDto?.Name);
            throw;
        }
    }

    public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            if (updateDto == null)
            {
                throw new ArgumentNullException(nameof(updateDto));
            }

            _logger.LogInformation("Updating category with ID: {Id}", id);

            var existingEntity = await _repository.GetByIdAsync(id, cancellationToken);
            if (existingEntity == null)
            {
                _logger.LogWarning("Category with ID: {Id} not found for update", id);
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            _mapper.Map(updateDto, existingEntity);
            existingEntity.ModifiedDate = DateTime.UtcNow;
            existingEntity.ModifiedBy = "System"; // TODO: Get from current user context

            var updatedEntity = await _repository.UpdateAsync(existingEntity, cancellationToken);
            var dto = _mapper.Map<CategoryDto>(updatedEntity);

            _logger.LogInformation("Successfully updated category with ID: {Id}", id);
            return dto;
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating category with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting category with ID: {Id}", id);

            var exists = await _repository.ExistsAsync(id, cancellationToken);
            if (!exists)
            {
                _logger.LogWarning("Category with ID: {Id} not found for deletion", id);
                return false;
            }

            await _repository.DeleteAsync(id, cancellationToken);
            _logger.LogInformation("Successfully deleted category with ID: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting category with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching categories with term: {SearchTerm}", searchTerm);
            var entities = await _repository.SearchAsync(searchTerm, cancellationToken);
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(entities);
            _logger.LogInformation("Successfully found {Count} categories matching search term: {SearchTerm}", dtos.Count(), searchTerm);
            return dtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching categories with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
