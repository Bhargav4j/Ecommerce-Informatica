using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly DbContext _context;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(DbContext context, ILogger<BrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active brands");
            return await _context.Set<Brand>()
                .AsNoTracking()
                .Where(b => b.IsActive)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all brands");
            throw;
        }
    }

    public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting brand with id {BrandId}", id);
            return await _context.Set<Brand>()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting brand with id {BrandId}", id);
            throw;
        }
    }

    public async Task<Brand> AddAsync(Brand entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new brand: {BrandName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Brand>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Brand added successfully with id {BrandId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding brand: {BrandName}", entity.Name);
            throw;
        }
    }

    public async Task<Brand> UpdateAsync(Brand entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating brand with id {BrandId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Brand>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Brand updated successfully with id {BrandId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating brand with id {BrandId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting brand with id {BrandId}", id);
            var entity = await _context.Set<Brand>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Brand with id {BrandId} not found", id);
                throw new InvalidOperationException($"Brand with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Brand>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Brand deleted successfully with id {BrandId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting brand with id {BrandId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if brand exists with id {BrandId}", id);
            return await _context.Set<Brand>()
                .AsNoTracking()
                .AnyAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if brand exists with id {BrandId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching brands with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Brand>()
                .AsNoTracking()
                .Where(b => b.IsActive &&
                    (b.Name.ToLower().Contains(lowerSearchTerm) ||
                     b.Description.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
