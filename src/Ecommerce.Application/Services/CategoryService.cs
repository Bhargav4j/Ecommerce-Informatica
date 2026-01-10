using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services;

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
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting category with ID: {CategoryId}", id);

            var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
            if (category == null)
            {
                _logger.LogWarning("Category with ID: {CategoryId} not found", id);
                return null;
            }

            return _mapper.Map<CategoryDto>(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting category with ID: {CategoryId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all categories");

            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all categories");
            throw;
        }
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Creating new category: {CategoryName}", categoryCreateDto.CategoryName);

            var category = _mapper.Map<Category>(categoryCreateDto);
            var createdCategory = await _categoryRepository.AddAsync(category, cancellationToken);

            _logger.LogInformation("Successfully created category with ID: {CategoryId}", createdCategory.CategoryId);

            return _mapper.Map<CategoryDto>(createdCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating category: {CategoryName}", categoryCreateDto.CategoryName);
            throw;
        }
    }

    public async Task<CategoryDto?> UpdateAsync(CategoryUpdateDto categoryUpdateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating category with ID: {CategoryId}", categoryUpdateDto.CategoryId);

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDto.CategoryId, cancellationToken);
            if (existingCategory == null)
            {
                _logger.LogWarning("Category with ID: {CategoryId} not found for update", categoryUpdateDto.CategoryId);
                return null;
            }

            _mapper.Map(categoryUpdateDto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory, cancellationToken);

            _logger.LogInformation("Successfully updated category with ID: {CategoryId}", categoryUpdateDto.CategoryId);

            return _mapper.Map<CategoryDto>(existingCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating category with ID: {CategoryId}", categoryUpdateDto.CategoryId);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting category with ID: {CategoryId}", id);

            if (!await _categoryRepository.ExistsAsync(id, cancellationToken))
            {
                _logger.LogWarning("Category with ID: {CategoryId} not found for deletion", id);
                return false;
            }

            await _categoryRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation("Successfully deleted category with ID: {CategoryId}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting category with ID: {CategoryId}", id);
            throw;
        }
    }
}
