using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(EcommerceDbContext context, ILogger<PersonRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.Province)
                .Where(p => p.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all persons");
            throw;
        }
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.Province)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with ID {PersonId}", id);
            throw;
        }
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Persons.AddAsync(person, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Person {PersonId} created successfully", person.Id);
            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding person {Username}", person.Username);
            throw;
        }
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Person {PersonId} updated successfully", person.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating person {PersonId}", person.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await _context.Persons.FindAsync(new object[] { id }, cancellationToken);
            if (person != null)
            {
                person.IsActive = false;
                person.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Person {PersonId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person {PersonId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons.AnyAsync(p => p.Id == id && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if person {PersonId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Person>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.Province)
                .Where(p => p.IsActive && (p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm) || p.Email.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching persons with term {SearchTerm}", searchTerm);
            throw;
        }
    }

    public async Task<Person?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .Include(p => p.City)
                .Include(p => p.Province)
                .FirstOrDefaultAsync(p => p.Username == username && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with username {Username}", username);
            throw;
        }
    }

    public async Task<bool> ValidateCredentialsAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await GetByUsernameAsync(username, cancellationToken);
            if (person == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, person.PasswordHash);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating credentials for username {Username}", username);
            throw;
        }
    }
}
