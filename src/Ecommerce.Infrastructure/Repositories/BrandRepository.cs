using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Brand entity
/// </summary>
public class BrandRepository : IBrandRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(EcommerceDbContext context, ILogger<BrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all brands");
            return await _context.Brands
                .Where(b => b.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all brands");
            throw;
        }
    }

    public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting brand with ID: {BrandId}", id);
            return await _context.Brands
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting brand with ID: {BrandId}", id);
            throw;
        }
    }

    public async Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new brand: {BrandName}", brand.Name);
            brand.CreatedDate = DateTime.UtcNow;
            brand.IsActive = true;
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync(cancellationToken);
            return brand;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding brand: {BrandName}", brand.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating brand with ID: {BrandId}", brand.Id);
            brand.ModifiedDate = DateTime.UtcNow;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating brand with ID: {BrandId}", brand.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting brand with ID: {BrandId}", id);
            var brand = await _context.Brands.FindAsync(new object[] { id }, cancellationToken);
            if (brand != null)
            {
                brand.IsActive = false;
                brand.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting brand with ID: {BrandId}", id);
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
            _logger.LogError(ex, "Error checking if brand exists with ID: {BrandId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Brand>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching brands with term: {SearchTerm}", searchTerm);
            return await _context.Brands
                .Where(b => b.IsActive &&
                    (b.Name.Contains(searchTerm) || (b.Description != null && b.Description.Contains(searchTerm))))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching brands with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
