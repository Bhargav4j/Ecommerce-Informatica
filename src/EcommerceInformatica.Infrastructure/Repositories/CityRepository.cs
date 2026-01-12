using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly DbContext _context;
    private readonly ILogger<CityRepository> _logger;

    public CityRepository(DbContext context, ILogger<CityRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active cities");
            return await _context.Set<City>()
                .AsNoTracking()
                .Where(c => c.IsActive)
                .Include(c => c.Province)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all cities");
            throw;
        }
    }

    public async Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting city with id {CityId}", id);
            return await _context.Set<City>()
                .AsNoTracking()
                .Include(c => c.Province)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting city with id {CityId}", id);
            throw;
        }
    }

    public async Task<City> AddAsync(City entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new city: {CityName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<City>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("City added successfully with id {CityId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding city: {CityName}", entity.Name);
            throw;
        }
    }

    public async Task<City> UpdateAsync(City entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating city with id {CityId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<City>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("City updated successfully with id {CityId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating city with id {CityId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting city with id {CityId}", id);
            var entity = await _context.Set<City>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("City with id {CityId} not found", id);
                throw new InvalidOperationException($"City with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<City>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("City deleted successfully with id {CityId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting city with id {CityId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if city exists with id {CityId}", id);
            return await _context.Set<City>()
                .AsNoTracking()
                .AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if city exists with id {CityId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<City>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching cities with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<City>()
                .AsNoTracking()
                .Where(c => c.IsActive &&
                    (c.Name.ToLower().Contains(lowerSearchTerm) ||
                     c.Description.ToLower().Contains(lowerSearchTerm)))
                .Include(c => c.Province)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching cities with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
