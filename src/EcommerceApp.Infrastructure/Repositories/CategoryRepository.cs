using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(EcommerceDbContext context, ILogger<CategoryRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Categories
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all categories");
            throw;
        }
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving category with id {CategoryId}", id);
            throw;
        }
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding category");
            throw;
        }
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with id {CategoryId}", category.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var category = await _context.Categories.FindAsync(new object[] { id }, cancellationToken);
            if (category != null)
            {
                category.IsActive = false;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting category with id {CategoryId}", id);
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
            _logger.LogError(ex, "Error checking if category exists with id {CategoryId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Category>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Categories
                .Where(c => c.IsActive && c.Name.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching categories with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
