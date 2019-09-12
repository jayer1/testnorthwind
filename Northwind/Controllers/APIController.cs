using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class APIController : Controller
    {
        // this controller depends on the NorthwindRepository (dependency injection)
        private INorthwindRepository repository;
        public APIController(INorthwindRepository repo) => repository = repo;

        [HttpGet, Route("api/product")] // This brings JSON (test in Postman)
        // returns list of all products
        public IEnumerable<Product> Get() => repository.Products.OrderBy(p => p.ProductName);

        [HttpGet, Route("api/product/{id}")]
        // returns specific product
        public Product Get(int id) => repository.Products.FirstOrDefault(p => p.ProductId == id);

        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => repository.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product")] 
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => repository.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}/maxprice/{maxprice}")]
        // returns all products in a specific category where discontinued = true/false
        // and where unitprice <= maxprice
        public IEnumerable<Product> GetByCategoryDiscontinuedPrice(int CategoryId, bool discontinued, int maxprice) => repository.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued && p.UnitPrice <= maxprice).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/order/notshipped")] //?
        // returns list of all orders not shipped
        public IEnumerable<Order> GetOrdersNotShippedYet() => repository.Orders.Where(o => o.ShippedDate == null).OrderBy(o => o.OrderDate);
        
        [HttpGet, Route("api/order/passedrequired")] 
        // returns list of all orders not shipped and past required
        public IEnumerable<Order> GetOrdersPassedReq() => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate < DateTime.Now).OrderBy(o => o.OrderDate);

        //FIX THIS TO BE REQUIRED WEEK
        [HttpGet, Route("api/order/requiredsoon")] 
        // returns list of all orders ordered by shipped date required in a week ??
        public IEnumerable<Order> GetOrdersRequiredSoon() => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate >= DateTime.Now.AddDays(-7)).OrderBy(o => o.OrderDate);


        [HttpGet, Route("api/order/requiredtoday")] 
        // returns list of all orders ordered by shipped date required today
        public IEnumerable<Order> GetOrdersRequiredToday() => repository.Orders.Where(o => o.ShippedDate == null).Where(o => o.RequiredDate == DateTime.Now).OrderBy(o => o.OrderDate);



    }
}
