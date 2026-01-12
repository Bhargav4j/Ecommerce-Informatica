using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly DbContext _context;
    private readonly ILogger<InvoiceRepository> _logger;

    public InvoiceRepository(DbContext context, ILogger<InvoiceRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active invoices");
            return await _context.Set<Invoice>()
                .AsNoTracking()
                .Where(i => i.IsActive)
                .Include(i => i.Person)
                .Include(i => i.PaymentMethod)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all invoices");
            throw;
        }
    }

    public async Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoice with id {InvoiceId}", id);
            return await _context.Set<Invoice>()
                .AsNoTracking()
                .Include(i => i.Person)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoice with id {InvoiceId}", id);
            throw;
        }
    }

    public async Task<Invoice> AddAsync(Invoice entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new invoice for person {PersonId}", entity.PersonId);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Invoice>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice added successfully with id {InvoiceId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding invoice for person {PersonId}", entity.PersonId);
            throw;
        }
    }

    public async Task<Invoice> UpdateAsync(Invoice entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating invoice with id {InvoiceId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Invoice>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice updated successfully with id {InvoiceId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invoice with id {InvoiceId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting invoice with id {InvoiceId}", id);
            var entity = await _context.Set<Invoice>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Invoice with id {InvoiceId} not found", id);
                throw new InvalidOperationException($"Invoice with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Invoice>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Invoice deleted successfully with id {InvoiceId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invoice with id {InvoiceId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if invoice exists with id {InvoiceId}", id);
            return await _context.Set<Invoice>()
                .AsNoTracking()
                .AnyAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if invoice exists with id {InvoiceId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Invoice>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching invoices with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Invoice>()
                .AsNoTracking()
                .Where(i => i.IsActive &&
                    (i.Person != null &&
                     (i.Person.FirstName.ToLower().Contains(lowerSearchTerm) ||
                      i.Person.LastName.ToLower().Contains(lowerSearchTerm) ||
                      i.Person.Email.ToLower().Contains(lowerSearchTerm))))
                .Include(i => i.Person)
                .Include(i => i.PaymentMethod)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching invoices with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
