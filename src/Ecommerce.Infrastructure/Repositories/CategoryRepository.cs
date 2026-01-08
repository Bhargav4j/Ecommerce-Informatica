using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Category entity
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(EcommerceDbContext context, ILogger<CategoryRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all categories");
            return await _context.Categories
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all categories");
            throw;
        }
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting category with ID: {CategoryId}", id);
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting category with ID: {CategoryId}", id);
            throw;
        }
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new category: {CategoryName}", category.Name);
            category.CreatedDate = DateTime.UtcNow;
            category.IsActive = true;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding category: {CategoryName}", category.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating category with ID: {CategoryId}", category.Id);
            category.ModifiedDate = DateTime.UtcNow;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with ID: {CategoryId}", category.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting category with ID: {CategoryId}", id);
            var category = await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
            if (category != null)
            {
                category.IsActive = false;
                category.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category with ID: {CategoryId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Categories.AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if category exists with ID: {CategoryId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Category>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching categories with term: {SearchTerm}", searchTerm);
            return await _context.Categories
                .Where(c => c.IsActive &&
                    (c.Name.Contains(searchTerm) || (c.Description != null && c.Description.Contains(searchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching categories with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
