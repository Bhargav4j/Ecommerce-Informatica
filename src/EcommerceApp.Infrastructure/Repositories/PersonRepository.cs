using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Interfaces.Repositories;
using EcommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceApp.Infrastructure.Repositories;

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

    public async Task<Person?> GetByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Dni == dni && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with DNI {Dni}", dni);
            throw;
        }
    }

    public async Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with email {Email}", email);
            throw;
        }
    }

    public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync(cancellationToken);
            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding person");
            throw;
        }
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating person with DNI {Dni}", person.Dni);
            throw;
        }
    }

    public async Task DeleteAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            var person = await _context.Persons.FindAsync(new object[] { dni }, cancellationToken);
            if (person != null)
            {
                person.IsActive = false;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person with DNI {Dni}", dni);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons.AnyAsync(p => p.Dni == dni && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if person exists with DNI {Dni}", dni);
            throw;
        }
    }

    public async Task<Person?> AuthenticateAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email && p.Password == password && p.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error authenticating person with email {Email}", email);
            throw;
        }
    }
}
