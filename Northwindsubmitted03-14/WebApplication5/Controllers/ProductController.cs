using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        // this controller depends on the BloggingRepository
        private INorthwindRepository repository;
        public ProductController(INorthwindRepository repo) => repository = repo;

        public IActionResult Category() => View(repository.Category.OrderBy(c => c.CategoryName));

        //public ActionResult Index() => View();
        public IActionResult Index(int id) => View(new CategoryProductsModel
        {
            Products = repository.Product.Where(p => p.CategoryId == id && p.Discontinued == false).OrderBy(p => p.ProductName)
        });
    }
}