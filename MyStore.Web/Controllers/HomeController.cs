using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Business.ProductsService;
using MyStore.DataAccess.Data;
using MyStore.Models.Models;
using MyStore.Web.Models;
using MyStore.Web.Utility;
using System.Diagnostics;

namespace MyStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductsService _productService;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService, ApplicationDbContext db)
        {
            _logger = logger;
            _productService = productsService;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }

        //[HttpPost]
        //[ActionName("Details")]
        //public ActionResult ProductDetails(int? id)
        //{
        //    List<Products> products = new List<Products>();
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = _db.products.Include(c => c.Category).FirstOrDefault(c => c.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    products = HttpContext.Session.Get<List<Products>>("products");
        //    if (products == null)
        //    {
        //        products = new List<Products>();
        //    }

        //    products.Add(product);
        //    HttpContext.Session.Set("products", products);
        //    return View(product);
        //}

        [HttpPost]
        [ActionName("Details")]
        public async Task<IActionResult> ProductDetails(int id)
        {
            List<Products> products = new List<Products>();
            var product = await _productService.GetProductById(id);

            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }

            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
        }

        [HttpGet]
        [ActionName("Remove")]
        public async Task<IActionResult> RemoveToCart(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");

            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}
