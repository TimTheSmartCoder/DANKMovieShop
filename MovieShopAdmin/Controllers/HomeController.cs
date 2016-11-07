using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using MovieShopAdmin.Models.Home;
using MovieShopBackend;

namespace MovieShopAdmin.Controllers
{
    public class HomeController : Controller
    {
        private IManager<Order> _OrdersManager = new ManagerFacade().GetOrderManager();
        private IManager<Customer> _CustomersManager = new ManagerFacade().GetCustomerManager();

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.LastFiveOrders = (this._OrdersManager.ReadAll().Count <= 5) ? this._OrdersManager.ReadAll() : this._OrdersManager.ReadAll().GetRange(0, 5);
            homeViewModel.NumberOfCustomers = this._CustomersManager.ReadAll().Count;
            homeViewModel.NumberOfOrders = this._OrdersManager.ReadAll().Count;
            
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}