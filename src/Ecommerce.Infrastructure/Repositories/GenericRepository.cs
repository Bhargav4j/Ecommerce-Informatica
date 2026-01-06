using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly EcommerceDbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ILogger<GenericRepository<T>> _logger;

    public GenericRepository(EcommerceDbContext context, ILogger<GenericRepository<T>> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all {EntityType} entities", typeof(T).Name);
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all {EntityType} entities", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting {EntityType} with ID: {Id}", typeof(T).Name, id);
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting {EntityType} with ID: {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new {EntityType} entity", typeof(T).Name);
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully added new {EntityType} entity", typeof(T).Name);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding {EntityType} entity", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating {EntityType} entity", typeof(T).Name);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfully updated {EntityType} entity", typeof(T).Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating {EntityType} entity", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting {EntityType} with ID: {Id}", typeof(T).Name, id);
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity != null)
            {
                var isActiveProperty = entity.GetType().GetProperty("IsActive");
                if (isActiveProperty != null)
                {
                    isActiveProperty.SetValue(entity, false);
                    await UpdateAsync(entity, cancellationToken);
                }
                else
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                _logger.LogInformation("Successfully deleted {EntityType} with ID: {Id}", typeof(T).Name, id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting {EntityType} with ID: {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity != null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if {EntityType} exists with ID: {Id}", typeof(T).Name, id);
            throw;
        }
    }
}
