using MyStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Business.ProductsService;

public interface IProductsService
{
    Task<List<Products>> GetProducts();
    Task<Products> GetProductById(int id);
    Task CreateProduct(Products product);
    Task UpdateProduct(Products product);
    Task DeleteProduct(int id);
    Task<Products> SearchProduct(string productName);
    //Task<Products> LowToHighPrice(decimal lowPrice, decimal highPrice);
}
