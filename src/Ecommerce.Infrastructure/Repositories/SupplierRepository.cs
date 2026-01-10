using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<SupplierRepository> _logger;

    public SupplierRepository(EcommerceDbContext context, ILogger<SupplierRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Supplier?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving supplier with ID: {SupplierId}", id);

            return await _context.Suppliers
                .Include(s => s.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving supplier with ID: {SupplierId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all suppliers");

            return await _context.Suppliers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all suppliers");
            throw;
        }
    }

    public async Task<Supplier> AddAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new supplier: {BusinessName}", supplier.BusinessName);

            await _context.Suppliers.AddAsync(supplier, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully added supplier with ID: {SupplierId}", supplier.Id);

            return supplier;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding supplier: {BusinessName}", supplier.BusinessName);
            throw;
        }
    }

    public async Task UpdateAsync(Supplier supplier, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating supplier with ID: {SupplierId}", supplier.Id);

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated supplier with ID: {SupplierId}", supplier.Id);
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
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted supplier with ID: {SupplierId}", id);
            }
            else
            {
                _logger.LogWarning("Supplier with ID: {SupplierId} not found for deletion", id);
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
            return await _context.Suppliers.AnyAsync(s => s.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of supplier with ID: {SupplierId}", id);
            throw;
        }
    }
}
