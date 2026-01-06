using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<ProviderRepository> _logger;

    public ProviderRepository(EcommerceDbContext context, ILogger<ProviderRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Provider>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Providers.Where(p => p.IsActive).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all providers");
            throw;
        }
    }

    public async Task<Provider?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Providers.FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving provider with ID {ProviderId}", id);
            throw;
        }
    }

    public async Task<Provider> AddAsync(Provider provider, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Providers.AddAsync(provider, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Provider {ProviderId} created successfully", provider.Id);
            return provider;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding provider {ProviderName}", provider.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Provider provider, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Provider {ProviderId} updated successfully", provider.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating provider {ProviderId}", provider.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var provider = await _context.Providers.FindAsync(new object[] { id }, cancellationToken);
            if (provider != null)
            {
                provider.IsActive = false;
                provider.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Provider {ProviderId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting provider {ProviderId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Providers.AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if provider {ProviderId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Provider>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Providers.Where(p => p.IsActive && p.Name.Contains(searchTerm)).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching providers with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
