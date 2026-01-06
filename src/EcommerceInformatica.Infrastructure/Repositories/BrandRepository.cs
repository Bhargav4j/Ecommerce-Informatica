using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(EcommerceDbContext context, ILogger<BrandRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Brands.Where(b => b.IsActive).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all brands");
            throw;
        }
    }

    public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving brand with ID {BrandId}", id);
            throw;
        }
    }

    public async Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Brands.AddAsync(brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Brand {BrandId} created successfully", brand.Id);
            return brand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding brand {BrandName}", brand.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Brand {BrandId} updated successfully", brand.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating brand {BrandId}", brand.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var brand = await _context.Brands.FindAsync(new object[] { id }, cancellationToken);
            if (brand != null)
            {
                brand.IsActive = false;
                brand.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Brand {BrandId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting brand {BrandId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Brands.AnyAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if brand {BrandId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Brands.Where(b => b.IsActive && b.Name.Contains(searchTerm)).AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching brands with term {SearchTerm}", searchTerm);
            throw;
        }
    }
}
