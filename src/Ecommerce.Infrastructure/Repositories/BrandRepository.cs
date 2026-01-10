using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<BrandRepository> _logger;

    public BrandRepository(EcommerceDbContext context, ILogger<BrandRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving brand with ID: {BrandId}", id);

            return await _context.Brands
                .Include(b => b.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving brand with ID: {BrandId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Retrieving all brands");

            return await _context.Brands
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all brands");
            throw;
        }
    }

    public async Task<Brand> AddAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new brand: {BrandName}", brand.Name);

            await _context.Brands.AddAsync(brand, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully added brand with ID: {BrandId}", brand.Id);

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

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated brand with ID: {BrandId}", brand.Id);
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
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Successfully deleted brand with ID: {BrandId}", id);
            }
            else
            {
                _logger.LogWarning("Brand with ID: {BrandId} not found for deletion", id);
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
            return await _context.Brands.AnyAsync(b => b.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of brand with ID: {BrandId}", id);
            throw;
        }
    }
}
