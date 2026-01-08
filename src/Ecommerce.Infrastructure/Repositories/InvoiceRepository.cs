using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Invoice entity
/// </summary>
public class InvoiceRepository : IInvoiceRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<InvoiceRepository> _logger;

    public InvoiceRepository(EcommerceDbContext context, ILogger<InvoiceRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all invoices");
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.PaymentMethod)
                .Where(i => i.IsActive)
                .AsNoTracking()
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
            _logger.LogInformation("Getting invoice with ID: {InvoiceId}", id);
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.PaymentMethod)
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoice with ID: {InvoiceId}", id);
            throw;
        }
    }

    public async Task<Invoice?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoice with details for ID: {InvoiceId}", id);
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.PaymentMethod)
                .Include(i => i.InvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoice with details for ID: {InvoiceId}", id);
            throw;
        }
    }

    public async Task<Invoice> AddAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new invoice: {InvoiceNumber}", invoice.InvoiceNumber);
            invoice.CreatedDate = DateTime.UtcNow;
            invoice.IsActive = true;
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync(cancellationToken);
            return invoice;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding invoice: {InvoiceNumber}", invoice.InvoiceNumber);
            throw;
        }
    }

    public async Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating invoice with ID: {InvoiceId}", invoice.Id);
            invoice.ModifiedDate = DateTime.UtcNow;
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invoice with ID: {InvoiceId}", invoice.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting invoice with ID: {InvoiceId}", id);
            var invoice = await _context.Invoices.FindAsync(new object[] { id }, cancellationToken);
            if (invoice != null)
            {
                invoice.IsActive = false;
                invoice.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invoice with ID: {InvoiceId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Invoices.AnyAsync(i => i.Id == id && i.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if invoice exists with ID: {InvoiceId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Invoice>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoices for customer ID: {CustomerId}", customerId);
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.PaymentMethod)
                .Where(i => i.CustomerId == customerId && i.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoices for customer ID: {CustomerId}", customerId);
            throw;
        }
    }

    public async Task<IEnumerable<Invoice>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting invoices between {StartDate} and {EndDate}", startDate, endDate);
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.PaymentMethod)
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate && i.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting invoices between {StartDate} and {EndDate}", startDate, endDate);
            throw;
        }
    }
}
