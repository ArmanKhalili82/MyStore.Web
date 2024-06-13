using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Business.OrderService;
using MyStore.DataAccess.Data;
using MyStore.Models.Models;
using MyStore.Web.Utility;

namespace MyStore.Web.Controllers;

public class OrderController : Controller
{
    private IOrderService _orderService;
    private readonly ApplicationDbContext _context;

    public OrderController(IOrderService orderService, ApplicationDbContext context)
    {
        _orderService = orderService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetOrders();
        return View(orders);
    }

    [HttpGet]
    public IActionResult CreateOrder()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        await _orderService.CreateOrder(order);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditOrder(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return View(order);
    }

    [HttpPost]
    public async Task<IActionResult> EditOrder(Order order)
    {
        await _orderService.UpdateOrder(order);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _orderService.GetOrderById(id);
        return View(order);
    }

    [HttpPost, ActionName("DeleteOrder")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.DeleteOrder(id);
        return RedirectToAction("Index");
    }





    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(Order order)
    {
        List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        if (products != null)
        {
            foreach (var product in products)
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.ProductId = product.Id;
                order.OrderDetails.Add(orderDetails);
            }
        }

        order.OrderNo = GetOrderNo();
        await _context.orders.AddAsync(order);
        await _context.SaveChangesAsync();
        HttpContext.Session.Set("products", new List<Products>());

        return View();
    }

    public string GetOrderNo()
    {
        int rowCount = _context.orders.ToList().Count() + 1;
        return rowCount.ToString("000");
    }
}
