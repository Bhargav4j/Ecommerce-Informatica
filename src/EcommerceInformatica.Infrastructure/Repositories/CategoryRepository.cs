using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbContext _context;
    private readonly ILogger<CategoryRepository> _logger;

    public CategoryRepository(DbContext context, ILogger<CategoryRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active categories");
            return await _context.Set<Category>()
                .AsNoTracking()
                .Where(c => c.IsActive)
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
            _logger.LogInformation("Getting category with id {CategoryId}", id);
            return await _context.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting category with id {CategoryId}", id);
            throw;
        }
    }

    public async Task<Category> AddAsync(Category entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new category: {CategoryName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Category>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category added successfully with id {CategoryId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding category: {CategoryName}", entity.Name);
            throw;
        }
    }

    public async Task<Category> UpdateAsync(Category entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating category with id {CategoryId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Category>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category updated successfully with id {CategoryId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating category with id {CategoryId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting category with id {CategoryId}", id);
            var entity = await _context.Set<Category>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Category with id {CategoryId} not found", id);
                throw new InvalidOperationException($"Category with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Category>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Category deleted successfully with id {CategoryId}", id);
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
            _logger.LogInformation("Checking if category exists with id {CategoryId}", id);
            return await _context.Set<Category>()
                .AsNoTracking()
                .AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
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
            _logger.LogInformation("Searching categories with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Category>()
                .AsNoTracking()
                .Where(c => c.IsActive &&
                    (c.Name.ToLower().Contains(lowerSearchTerm) ||
                     c.Description.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching categories with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
