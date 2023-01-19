using ASP.NET_APPLICATION.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class ProductsController : Controller
    {

        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 0, Name = "Apple", Price = 1.49m, Calories = 130, Sugar = 31},
            new Product { Id = 1, Name = "Banana", Price = 1.99m, Calories = 153, Sugar = 21 },
            new Product { Id = 2, Name = "Orange", Price = 2.19m, Calories = 93, Sugar = 52 },
            new Product { Id = 3, Name = "Potato", Price = 0.49m, Calories = 30, Sugar = 21},
            new Product { Id = 4, Name = "Cherry", Price = 2.19m, Calories = 53, Sugar = 61 },
            new Product { Id = 5, Name = "Tomato", Price = 0.89m, Calories = 43, Sugar = 52 },
        };

        // GET: /Products
        public ActionResult Index()
        {
            return View(_products);
        }

        // GET: /Products/Product/3
        public ActionResult Product(int Id)
        {
            var product = _products.FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        // GET: /Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Price,Name,Sugar,Calories")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var maxId = _products.Max(p => p.Id);
                    product.Id = maxId + 1;
                    _products.Add(product);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        // GET: /Products/Edit/3
        public ActionResult Edit(int Id)
        {
            var product = _products.FirstOrDefault(p => p.Id == Id);
            return View(product);
        }

        // POST: /Products/Edit/3
        [HttpPost]
        public ActionResult Edit(int Id, Product product)
        {
            try
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == Id);
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Sugar = product.Sugar;
                existingProduct.Calories = product.Calories;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // POST: /Products/Delete/3
        public ActionResult Delete(int Id)
        {
            try
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == Id);
                _products.Remove(existingProduct);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Search
        [HttpPost]
        public ActionResult Search(string name, decimal? minPrice, decimal? maxPrice)
        {
            ViewBag.Name = name;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            var products = _products.Where(p => p.Name.Contains(name ?? "") && (!minPrice.HasValue || p.Price >= minPrice) && (!maxPrice.HasValue || p.Price <= maxPrice));
            return View("Index", products);
        }
    }
}