using AutoMapper;
using EcommerceInformatica.Application.DTOs;
using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Application.Services;

/// <summary>
/// Service implementation for Category operations
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<object>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all categories");
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all categories");
            throw;
        }
    }

    public async Task<object?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving category with id {Id}", id);
            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category with id {Id}", id);
            throw;
        }
    }

    public async Task<object> CreateAsync(object categoryCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var dto = (CategoryCreateDto)categoryCreateDto;
            _logger.LogInformation("Creating new category: {Name}", dto.Name);
            var category = _mapper.Map<Category>(dto);
            var created = await _categoryRepository.AddAsync(category, cancellationToken);
            return _mapper.Map<CategoryDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            throw;
        }
    }

    public async Task UpdateAsync(int id, object categoryUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var dto = (CategoryUpdateDto)categoryUpdateDto;
            _logger.LogInformation("Updating category with id {Id}", id);
            var existingCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found");
            }
            _mapper.Map(dto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with id {Id}", id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting category with id {Id}", id);
            await _categoryRepository.DeleteAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category with id {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<object>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching categories with term: {SearchTerm}", searchTerm);
            var categories = await _categoryRepository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching categories");
            throw;
        }
    }
}
