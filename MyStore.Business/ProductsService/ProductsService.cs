using Microsoft.EntityFrameworkCore;
using MyStore.DataAccess.Data;
using MyStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.ProductsService;

public class ProductsService: IProductsService
{
    private readonly ApplicationDbContext _context;

    public ProductsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Products products)
    {
        await _context.products.AddAsync(products);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(int productId)
    {
        var product = await _context.products.FindAsync(productId);
        _context.products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Products> GetProductById(int id)
    {
        var product = await _context.products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        return product;
    }

    public async Task<List<Products>> GetProducts()
    {
        var products = await _context.products.Include(x => x.Category).ToListAsync();
        return products;
    }

    //public Task<Products> LowToHighPrice(decimal lowPrice, decimal highPrice)
    //{
    //    var products = _context.products.Include(x => x.Category)
    //        .Where(x => x.Price >= lowPrice && x.Price <= highPrice).ToList();
    //    return products;
    //}

    public async Task<Products> SearchProduct(string productName)
    {
        var product = await _context.products.FindAsync(productName);
        return product;
    }

    public async Task UpdateProduct(Products product)
    {
        _context.products.Update(product);
        await _context.SaveChangesAsync();
    }
}
