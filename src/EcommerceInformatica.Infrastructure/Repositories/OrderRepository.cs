using EcommerceInformatica.Domain.Entities;
using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommerceInformatica.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly EcommerceDbContext _context;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(EcommerceDbContext context, ILogger<OrderRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.PaymentMethod)
                .Include(o => o.OrderDetails)
                .Where(o => o.IsActive)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all orders");
            throw;
        }
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.PaymentMethod)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id && o.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order with ID {OrderId}", id);
            throw;
        }
    }

    public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Order {OrderId} created successfully", order.Id);
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding order");
            throw;
        }
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Order {OrderId} updated successfully", order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order {OrderId}", order.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var order = await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
            if (order != null)
            {
                order.IsActive = false;
                order.ModifiedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Order {OrderId} deleted successfully", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting order {OrderId}", id);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Orders.AnyAsync(o => o.Id == id && o.IsActive, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if order {OrderId} exists", id);
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetByPersonAsync(int personId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.PaymentMethod)
                .Include(o => o.OrderDetails)
                .Where(o => o.IsActive && o.PersonId == personId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders for person {PersonId}", personId);
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Orders
                .Include(o => o.Person)
                .Include(o => o.PaymentMethod)
                .Include(o => o.OrderDetails)
                .Where(o => o.IsActive && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders by date range");
            throw;
        }
    }
}
