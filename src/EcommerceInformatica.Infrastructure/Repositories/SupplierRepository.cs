using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly DbContext _context;
    private readonly ILogger<SupplierRepository> _logger;

    public SupplierRepository(DbContext context, ILogger<SupplierRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active suppliers");
            return await _context.Set<Supplier>()
                .AsNoTracking()
                .Where(s => s.IsActive)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all suppliers");
            throw;
        }
    }

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting supplier with id {SupplierId}", id);
            return await _context.Set<Supplier>()
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting supplier with id {SupplierId}", id);
            throw;
        }
    }

    public async Task<Supplier> AddAsync(Supplier entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new supplier: {SupplierName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Supplier>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Supplier added successfully with id {SupplierId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding supplier: {SupplierName}", entity.Name);
            throw;
        }
    }

    public async Task<Supplier> UpdateAsync(Supplier entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating supplier with id {SupplierId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Supplier>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Supplier updated successfully with id {SupplierId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating supplier with id {SupplierId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting supplier with id {SupplierId}", id);
            var entity = await _context.Set<Supplier>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Supplier with id {SupplierId} not found", id);
                throw new InvalidOperationException($"Supplier with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Supplier>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Supplier deleted successfully with id {SupplierId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting supplier with id {SupplierId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if supplier exists with id {SupplierId}", id);
            return await _context.Set<Supplier>()
                .AsNoTracking()
                .AnyAsync(s => s.Id == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if supplier exists with id {SupplierId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Supplier>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching suppliers with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Supplier>()
                .AsNoTracking()
                .Where(s => s.IsActive &&
                    (s.Name.ToLower().Contains(lowerSearchTerm) ||
                     s.Description.ToLower().Contains(lowerSearchTerm) ||
                     s.ContactName.ToLower().Contains(lowerSearchTerm) ||
                     s.ContactEmail.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching suppliers with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
