using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(DbContext context, ILogger<ProductRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active products");
            return await _context.Set<Product>()
                .AsNoTracking()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all products");
            throw;
        }
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting product with id {ProductId}", id);
            return await _context.Set<Product>()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product with id {ProductId}", id);
            throw;
        }
    }

    public async Task<Product> AddAsync(Product entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new product: {ProductName}", entity.Name);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Product>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Product added successfully with id {ProductId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product: {ProductName}", entity.Name);
            throw;
        }
    }

    public async Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating product with id {ProductId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Product>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Product updated successfully with id {ProductId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with id {ProductId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting product with id {ProductId}", id);
            var entity = await _context.Set<Product>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Product with id {ProductId} not found", id);
                throw new InvalidOperationException($"Product with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Product>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Product deleted successfully with id {ProductId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with id {ProductId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if product exists with id {ProductId}", id);
            return await _context.Set<Product>()
                .AsNoTracking()
                .AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if product exists with id {ProductId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Product>()
                .AsNoTracking()
                .Where(p => p.IsActive &&
                    (p.Name.ToLower().Contains(lowerSearchTerm) ||
                     p.Description.ToLower().Contains(lowerSearchTerm)))
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
