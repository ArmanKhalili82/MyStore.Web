using MyStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.OrderService;

public interface IOrderService
{
    Task<List<Order>> GetOrders();
    Task<Order> GetOrderById(int id);
    Task CreateOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
}
