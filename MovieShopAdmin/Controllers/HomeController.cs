using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using MovieShopAdmin.Models.Home;
using MovieShopBackend;
using ServiceGateway;

namespace MovieShopAdmin.Controllers
{
    public class HomeController : Controller
    {
        private IServiceGateway<Order> _OrdersManager = ServiceGatewayFactory.GetService<Order>();
        private IServiceGateway<Customer> _CustomersManager = ServiceGatewayFactory.GetService<Customer>();

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