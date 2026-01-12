using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class InvoiceDetailRepository : IInvoiceDetailRepository
{
    private readonly DbContext _context;
    private readonly ILogger<InvoiceDetailRepository> _logger;

    public InvoiceDetailRepository(DbContext context, ILogger<InvoiceDetailRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<InvoiceDetail>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active invoice details");
            return await _context.Set<InvoiceDetail>()
                .AsNoTracking()
                .Where(d => d.IsActive)
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all invoice details");
            throw;
        }
    }

    public async Task<InvoiceDetail?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoice detail with id {InvoiceDetailId}", id);
            return await _context.Set<InvoiceDetail>()
                .AsNoTracking()
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoice detail with id {InvoiceDetailId}", id);
            throw;
        }
    }

    public async Task<InvoiceDetail> AddAsync(InvoiceDetail entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new invoice detail for invoice {InvoiceId}", entity.InvoiceId);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<InvoiceDetail>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice detail added successfully with id {InvoiceDetailId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding invoice detail for invoice {InvoiceId}", entity.InvoiceId);
            throw;
        }
    }

    public async Task<InvoiceDetail> UpdateAsync(InvoiceDetail entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating invoice detail with id {InvoiceDetailId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<InvoiceDetail>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice detail updated successfully with id {InvoiceDetailId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invoice detail with id {InvoiceDetailId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting invoice detail with id {InvoiceDetailId}", id);
            var entity = await _context.Set<InvoiceDetail>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Invoice detail with id {InvoiceDetailId} not found", id);
                throw new InvalidOperationException($"Invoice detail with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<InvoiceDetail>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice detail deleted successfully with id {InvoiceDetailId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invoice detail with id {InvoiceDetailId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if invoice detail exists with id {InvoiceDetailId}", id);
            return await _context.Set<InvoiceDetail>()
                .AsNoTracking()
                .AnyAsync(d => d.Id == id && d.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if invoice detail exists with id {InvoiceDetailId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<InvoiceDetail>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching invoice details with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<InvoiceDetail>()
                .AsNoTracking()
                .Where(d => d.IsActive &&
                    (d.Product != null &&
                     d.Product.Name.ToLower().Contains(lowerSearchTerm)))
                .Include(d => d.Invoice)
                .Include(d => d.Product)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching invoice details with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
