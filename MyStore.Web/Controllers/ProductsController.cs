using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Business.ProductsService;
using MyStore.DataAccess.Data;
using MyStore.Models.Models;

namespace MyStore.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private IProductsService _productsService;
    private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

    public ProductsController(IProductsService productsService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, ApplicationDbContext context)
    {
        _productsService = productsService;
        _environment = environment;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productsService.GetProducts();
        return View(products);
    }

    //[HttpPost]
    //public async Task<IActionResult> Index(string productName)
    //{
    //    var product = await _productsService.SearchProduct(productName);
    //    return View(product);
    //}

    [HttpPost]
    public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
    {
        var products = _context.products.Include(x => x.Category)
            .Where(x => x.Price >= lowAmount && x.Price <= largeAmount).ToList();
        if (lowAmount == null || largeAmount == null)
        {
            products = _context.products.Include(x => x.Category).ToList();
        }
        return View(products);
    }

    [HttpGet]
    public IActionResult CreateProduct()
    {
        ViewData["CategoryId"] = new SelectList(_context.categories.ToList(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Products products, IFormFile image)
    {
        if (image!= null)
        {
            var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
            await image.CopyToAsync(new FileStream(name, FileMode.Create));
            products.Image = "Images/" + image.FileName;
        }
        await _productsService.CreateProduct(products);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        ViewData["CategoryId"] = new SelectList(_context.categories.ToList(), "Id", "Name");
        var product = await _productsService.GetProductById(id);
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(Products product, IFormFile image)
    {
        if (image != null)
        {

            //var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
            var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
            await image.CopyToAsync(new FileStream(name, FileMode.Create));
            product.Image = "Images/" + image.FileName;

            //if (!string.IsNullOrEmpty(product.Image))
            //{
            //    //Delete The Old Image
            //    var oldImagePath = Path.Combine(name, product.Image.TrimStart('\\'));

            //    if (System.IO.File.Exists(oldImagePath))
            //    {
            //        System.IO.File.Delete(oldImagePath);
            //    }

            //    await image.CopyToAsync(new FileStream(name, FileMode.Create));
            //    product.Image = "Images/" + image.FileName;
            //}
        }
        await _productsService.UpdateProduct(product);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productsService.GetProductById(id);
        return View(product);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productsService.DeleteProduct(id);
        return RedirectToAction("Index");
    }
}
