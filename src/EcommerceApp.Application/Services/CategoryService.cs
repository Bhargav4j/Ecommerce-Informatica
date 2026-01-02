using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Interfaces;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository repository, IMapper mapper, ILogger<CategoryService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var categories = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all categories");
            throw;
        }
    }

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var category = await _repository.GetByIdAsync(id, cancellationToken);
            return category != null ? _mapper.Map<CategoryDto>(category) : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category with id {CategoryId}", id);
            throw;
        }
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateDto createDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var category = _mapper.Map<Category>(createDto);
            var created = await _repository.AddAsync(category, cancellationToken);
            return _mapper.Map<CategoryDto>(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating category");
            throw;
        }
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                throw new InvalidOperationException($"Category with id {id} not found");
            }

            _mapper.Map(updateDto, existing);
            existing.Id = id;
            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with id {CategoryId}", id);
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
            _logger.LogError(ex, "Error deleting category with id {CategoryId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            var categories = await _repository.SearchAsync(searchTerm, cancellationToken);
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching categories with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
