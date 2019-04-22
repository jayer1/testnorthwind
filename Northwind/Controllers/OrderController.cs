using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class OrderController : Controller
    {
        private INorthwindRepository repository;
        public OrderController(INorthwindRepository repo) => repository = repo;
        // new
        //public int PageSize = 4;
        public ActionResult Index() 
        {
            return View(repository.Orders.Where(o => o.ShippedDate != null && o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate)
                .Skip(10).Take(5));
        }

        //public ActionResult Index() => View(repository.Orders.Where(o => o.ShippedDate != null && o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate).Skip(5).Take(5));
       
        //public ActionResult Index() => View(repository.Orders.Where(o => o.RequiredDate <= DateTime.Now));
    }
}