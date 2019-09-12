using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class OrderController : Controller
    {
        private INorthwindRepository repository;
        public OrderController(INorthwindRepository repo) => repository = repo;
        // private readonly int PageSize = 6;
        // new
        //public int PageSize = 4;
<<<<<<< HEAD
        public ActionResult Index(string searchShipName) 
        {
            if (searchShipName == null)
            {
                return View(null);
            }
            // || searchShipName == ""
            var orders = repository.Orders.Where(o => o.ShipName.StartsWith(searchShipName));
            return View(orders);           
=======

        //public ViewResult Index(int page = 1) => View(new OrderListViewModel
        //        //{
        //        //    Orders = repository.Orders
        //        //        .OrderByDescending(o => o.RequiredDate)
        //        //        .Skip((page - 1) * PageSize)
        //        //        .Take(PageSize),
        //        //    PagingInfo = new PagingInfo
        //        //    {
        //        //        CurrentPage = page,
        //        //        ItemsPerPage = PageSize,
        //        //        TotalItems = repository.Orders.Count()
        //        //    }
        //        //});

        //public ActionResult Index()
        //{
        //    return View(repository.Orders.Where(o => o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate).Skip(10).Take(5));
        //}
        public ActionResult Index(string searchShipName)

        {

            return View(repository.Orders.Where(o => o.ShipName.StartsWith(searchShipName) || searchShipName == "Vins et alcools Chevalier").ToList());

>>>>>>> 4eb4035997ec1071d72431d4216e0571285cccce
        }

        public IActionResult OrderDetail(int id) => View(repository.OrderDetails.FirstOrDefault(od => od.OrderID == id));


        //public ActionResult Index() => View(repository.Orders.Where(o => o.ShippedDate != null && o.RequiredDate < DateTime.Now).OrderBy(o => o.RequiredDate).Skip(5).Take(5));

        //public ActionResult Index() => View(repository.Orders.Where(o => o.RequiredDate <= DateTime.Now));

    }
}

