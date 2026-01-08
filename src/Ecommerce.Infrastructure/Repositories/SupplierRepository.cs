using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Supplier entity
/// </summary>
public class SupplierRepository : ISupplierRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<SupplierRepository> _logger;

    public SupplierRepository(EcommerceDbContext context, ILogger<SupplierRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all suppliers");
            return await _context.Suppliers
                .Where(s => s.IsActive)
                .AsNoTracking()
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
            _logger.LogInformation("Getting supplier with ID: {SupplierId}", id);
            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.Id == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting supplier with ID: {SupplierId}", id);
            throw;
        }
    }

    public async Task<Supplier> AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new supplier: {SupplierName}", supplier.Name);
            supplier.CreatedDate = DateTime.UtcNow;
            supplier.IsActive = true;
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync(cancellationToken);
            return supplier;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding supplier: {SupplierName}", supplier.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating supplier with ID: {SupplierId}", supplier.Id);
            supplier.ModifiedDate = DateTime.UtcNow;
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating supplier with ID: {SupplierId}", supplier.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting supplier with ID: {SupplierId}", id);
            var supplier = await _context.Suppliers.FindAsync(new object[] { id }, cancellationToken);
            if (supplier != null)
            {
                supplier.IsActive = false;
                supplier.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting supplier with ID: {SupplierId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Suppliers.AnyAsync(s => s.Id == id && s.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if supplier exists with ID: {SupplierId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Supplier>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching suppliers with term: {SearchTerm}", searchTerm);
            return await _context.Suppliers
                .Where(s => s.IsActive &&
                    (s.Name.Contains(searchTerm) ||
                     (s.ContactPerson != null && s.ContactPerson.Contains(searchTerm)) ||
                     (s.Email != null && s.Email.Contains(searchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching suppliers with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
