using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DbContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(DbContext context, ILogger<PersonRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all active persons");
            return await _context.Set<Person>()
                .AsNoTracking()
                .Where(p => p.IsActive)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all persons");
            throw;
        }
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting person with id {PersonId}", id);
            return await _context.Set<Person>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting person with id {PersonId}", id);
            throw;
        }
    }

    public async Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new person: {PersonEmail}", entity.Email);
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;

            await _context.Set<Person>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Person added successfully with id {PersonId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding person: {PersonEmail}", entity.Email);
            throw;
        }
    }

    public async Task<Person> UpdateAsync(Person entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating person with id {PersonId}", entity.Id);
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Person>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Person updated successfully with id {PersonId}", entity.Id);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating person with id {PersonId}", entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting person with id {PersonId}", id);
            var entity = await _context.Set<Person>().FindAsync(new object[] { id }, cancellationToken);

            if (entity == null)
            {
                _logger.LogWarning("Person with id {PersonId} not found", id);
                throw new InvalidOperationException($"Person with id {id} not found");
            }

            entity.IsActive = false;
            entity.ModifiedDate = DateTime.UtcNow;

            _context.Set<Person>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Person deleted successfully with id {PersonId}", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person with id {PersonId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Checking if person exists with id {PersonId}", id);
            return await _context.Set<Person>()
                .AsNoTracking()
                .AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if person exists with id {PersonId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Person>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching persons with term: {SearchTerm}", searchTerm);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync(cancellationToken);
            }

            var lowerSearchTerm = searchTerm.ToLower();

            return await _context.Set<Person>()
                .AsNoTracking()
                .Where(p => p.IsActive &&
                    (p.FirstName.ToLower().Contains(lowerSearchTerm) ||
                     p.LastName.ToLower().Contains(lowerSearchTerm) ||
                     p.Email.ToLower().Contains(lowerSearchTerm) ||
                     p.Dni.ToLower().Contains(lowerSearchTerm)))
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching persons with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
