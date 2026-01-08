using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Customer entity
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(EcommerceDbContext context, ILogger<CustomerRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting all customers");
            return await _context.Customers
                .Where(c => c.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all customers");
            throw;
        }
    }

    public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting customer with ID: {CustomerId}", id);
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer with ID: {CustomerId}", id);
            throw;
        }
    }

    public async Task<Customer?> GetByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting customer with DNI: {Dni}", dni);
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Dni == dni && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer with DNI: {Dni}", dni);
            throw;
        }
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Getting customer with email: {Email}", email);
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer with email: {Email}", email);
            throw;
        }
    }

    public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Adding new customer: {CustomerName}", $"{customer.FirstName} {customer.LastName}");
            customer.CreatedDate = DateTime.UtcNow;
            customer.IsActive = true;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding customer: {CustomerName}", $"{customer.FirstName} {customer.LastName}");
            throw;
        }
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Updating customer with ID: {CustomerId}", customer.Id);
            customer.ModifiedDate = DateTime.UtcNow;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer with ID: {CustomerId}", customer.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Deleting customer with ID: {CustomerId}", id);
            var customer = await _context.Customers.FindAsync(new object[] { id }, cancellationToken);
            if (customer != null)
            {
                customer.IsActive = false;
                customer.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer with ID: {CustomerId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Customers.AnyAsync(c => c.Id == id && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if customer exists with ID: {CustomerId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsByDniAsync(string dni, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Customers.AnyAsync(c => c.Dni == dni && c.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if customer exists with DNI: {Dni}", dni);
            throw;
        }
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Searching customers with term: {SearchTerm}", searchTerm);
            return await _context.Customers
                .Where(c => c.IsActive &&
                    (c.FirstName.Contains(searchTerm) ||
                     c.LastName.Contains(searchTerm) ||
                     c.Dni.Contains(searchTerm) ||
                     c.Email.Contains(searchTerm)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching customers with term: {SearchTerm}", searchTerm);
            throw;
        }
    }
}
