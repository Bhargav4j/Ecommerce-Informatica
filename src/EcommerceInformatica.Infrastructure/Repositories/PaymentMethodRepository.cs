using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly DbContext _context;
    private readonly ILogger<PaymentMethodRepository> _logger;

    public PaymentMethodRepository(DbContext context, ILogger<PaymentMethodRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active payment methods");
            return await _context.Set<PaymentMethod>()
                .AsNoTracking()
                .Where(pm => pm.IsActive)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all payment methods");
            throw;
        }
    }

    public async Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting payment method with id {PaymentMethodId}", id);
            return await _context.Set<PaymentMethod>()
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.Id == id && pm.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting payment method with id {PaymentMethodId}", id);
            throw;
        }
    }

    public async Task<PaymentMethod> AddAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new payment method: {PaymentMethodName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<PaymentMethod>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Payment method added successfully with id {PaymentMethodId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding payment method: {PaymentMethodName}", entity.Name);
            throw;
        }
    }

    public async Task<PaymentMethod> UpdateAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating payment method with id {PaymentMethodId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<PaymentMethod>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Payment method updated successfully with id {PaymentMethodId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating payment method with id {PaymentMethodId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting payment method with id {PaymentMethodId}", id);
            var entity = await _context.Set<PaymentMethod>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Payment method with id {PaymentMethodId} not found", id);
                throw new InvalidOperationException($"Payment method with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<PaymentMethod>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Payment method deleted successfully with id {PaymentMethodId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting payment method with id {PaymentMethodId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if payment method exists with id {PaymentMethodId}", id);
            return await _context.Set<PaymentMethod>()
                .AsNoTracking()
                .AnyAsync(pm => pm.Id == id && pm.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if payment method exists with id {PaymentMethodId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<PaymentMethod>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching payment methods with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<PaymentMethod>()
                .AsNoTracking()
                .Where(pm => pm.IsActive &&
                    (pm.Name.ToLower().Contains(lowerSearchTerm) ||
                     pm.Description.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching payment methods with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
