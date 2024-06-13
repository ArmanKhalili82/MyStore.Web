using Microsoft.EntityFrameworkCore;
using MyStore.DataAccess.Data;
using MyStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.OrderService;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrder(Order order)
    {
        await _context.orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrder(int orderId)
    {
        var order = await _context.orders.FindAsync(orderId);
        _context.orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _context.orders.FindAsync(id);
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _context.orders.ToListAsync();
        return orders;
    }

    public async Task UpdateOrder(Order order)
    {
        _context.orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
