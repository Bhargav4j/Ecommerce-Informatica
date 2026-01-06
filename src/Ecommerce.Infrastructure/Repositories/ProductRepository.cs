using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(EcommerceDbContext context, ILogger<GenericRepository<Product>> logger)
        : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active products with related data");
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all products");
            throw;
        }
    }

    public override async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting product with ID: {Id}", id);
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product with ID: {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync(cancellationToken);

            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.Description.Contains(searchTerm))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products with term: {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting products by category ID: {CategoryId}", categoryId);
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting products by category ID: {CategoryId}", categoryId);
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetByBrandIdAsync(int brandId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting products by brand ID: {BrandId}", brandId);
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.BrandId == brandId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting products by brand ID: {BrandId}", brandId);
            throw;
        }
    }
}
