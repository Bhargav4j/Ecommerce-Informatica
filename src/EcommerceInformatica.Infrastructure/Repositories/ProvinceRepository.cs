using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class ProvinceRepository : IProvinceRepository
{
    private readonly DbContext _context;
    private readonly ILogger<ProvinceRepository> _logger;

    public ProvinceRepository(DbContext context, ILogger<ProvinceRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active provinces");
            return await _context.Set<Province>()
                .AsNoTracking()
                .Where(p => p.IsActive)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all provinces");
            throw;
        }
    }

    public async Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting province with id {ProvinceId}", id);
            return await _context.Set<Province>()
                .AsNoTracking()
                .Include(p => p.Cities)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting province with id {ProvinceId}", id);
            throw;
        }
    }

    public async Task<Province> AddAsync(Province entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new province: {ProvinceName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Province>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Province added successfully with id {ProvinceId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding province: {ProvinceName}", entity.Name);
            throw;
        }
    }

    public async Task<Province> UpdateAsync(Province entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating province with id {ProvinceId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Province>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Province updated successfully with id {ProvinceId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating province with id {ProvinceId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting province with id {ProvinceId}", id);
            var entity = await _context.Set<Province>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Province with id {ProvinceId} not found", id);
                throw new InvalidOperationException($"Province with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Province>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Province deleted successfully with id {ProvinceId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting province with id {ProvinceId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if province exists with id {ProvinceId}", id);
            return await _context.Set<Province>()
                .AsNoTracking()
                .AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if province exists with id {ProvinceId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Province>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching provinces with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Province>()
                .AsNoTracking()
                .Where(p => p.IsActive &&
                    (p.Name.ToLower().Contains(lowerSearchTerm) ||
                     p.Description.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching provinces with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
