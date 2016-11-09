using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using MovieShopBackend;
using MovieShopUser.Models.Cart;
using MovieShopUser.Models.Checkout;
using ServiceGateway;

namespace MovieShopUser.Controllers
{
    public class CheckoutController : Controller
    {
        private IServiceGateway<Customer> _customerManager = ServiceGatewayFactory.GetService<Customer>();
        private IServiceGateway<Address> _addressManager = ServiceGatewayFactory.GetService<Address>();
        private IServiceGateway<Order> _OrderManager = ServiceGatewayFactory.GetService<Order>();

        // GET: Checkout
        public ActionResult Index()
        {
            return RedirectToAction("Email");
        }

        [HttpGet]
        public ActionResult Email()
        {
            return View(new CheckoutEmailViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email([Bind(Include = "Email")] CheckoutEmailViewModel checkoutEmailViewModel)
        {
            if (checkoutEmailViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                //Be aware off that we are only getting all of the customers here, because we do not want to
                //mess up are interfaces, normally you would get the id of the logged in user and use the id of
                //the user to get the customer we are currently viewing.
                Customer customer =
                    this._customerManager.ReadAll()
                        .FirstOrDefault(c => c.Email == checkoutEmailViewModel.Email);

                return RedirectToAction("Process", new { email = checkoutEmailViewModel.Email});
            }

            return View(checkoutEmailViewModel);
        }

        [HttpGet]
        public ActionResult Process(string email)
        {
            //Be aware off that we are only getting all of the customers here, because we do not want to
            //mess up are interfaces, normally you would get the id of the logged in user and use the id of
            //the user to get the customer we are currently viewing.
            Customer customer =
                this._customerManager.ReadAll()
                    .FirstOrDefault(c => c.Email == email);

            CheckoutProcessViewModel checkoutProcessViewModel;

            if (customer != null)
            {
                checkoutProcessViewModel = AutoMapper.Mapper.Map<CheckoutProcessViewModel>(customer);
                AutoMapper.Mapper.Map(customer.Address, checkoutProcessViewModel);
            }
            else
                checkoutProcessViewModel = new CheckoutProcessViewModel();

            return View(checkoutProcessViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Process([Bind(Include = "Id,FirstName,LastName,Email,StreetName,StreetNumber,Country,ZipCode")] CheckoutProcessViewModel checkoutProcessViewModel)
        {
            if (checkoutProcessViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                Customer customer = AutoMapper.Mapper.Map<Customer>(checkoutProcessViewModel);
                customer.Address = AutoMapper.Mapper.Map<Address>(checkoutProcessViewModel);

                if (checkoutProcessViewModel.Id == -1)
                {
                    Customer c = this._customerManager.Create(customer);
                    ShoppingCart shoppingCart = new ShoppingCart(this.HttpContext);

                    //TEMPORARY -------
                    //List<Movie> movies = new List<Movie>();
                    //foreach (var movie in shoppingCart.GetMoviesInCart())
                    //{
                    //    movie.Orders = null;
                    //    movies.Add(movie);
                    //}
                    //TEMPORARY -------

                    Order order = new Order()
                    {
                        CustomerId = c.Id,
                        Date = DateTime.Now,
                        Movies = shoppingCart.GetMoviesInCart()
                    };

                    this._OrderManager.Create(order);
                    shoppingCart.ClearCart();
                }
                else
                {
                    Customer c = this._customerManager.Update(customer);
                    c.Address = this._addressManager.Update(customer.Address);
                    ShoppingCart shoppingCart = new ShoppingCart(this.HttpContext);

                    //TEMPORARY -------
                    //List<Movie> movies = new List<Movie>();
                    //foreach (var movie in shoppingCart.GetMoviesInCart())
                    //{
                    //    movie.Orders = null;
                    //    movies.Add(movie);
                    //}
                    //TEMPORARY -------

                    Order order = new Order()
                    {
                        CustomerId = c.Id,
                        Date = DateTime.Now,
                        Movies = shoppingCart.GetMoviesInCart()
                    };

                    this._OrderManager.Create(order);
                    shoppingCart.ClearCart();
                }
                
                return RedirectToAction("Success");
            }

            return View(checkoutProcessViewModel);
        }

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }
    }
}