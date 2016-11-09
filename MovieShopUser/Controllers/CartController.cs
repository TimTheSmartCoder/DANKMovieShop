using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Entities;
using MovieShopBackend;
using MovieShopUser.Models.Cart;
using ServiceGateway;

namespace MovieShopUser.Controllers
{
    public class CartController : Controller
    {
        private ManagerFacade facade = new ManagerFacade();
        private IServiceGateway<Movie> movieManager = ServiceGatewayFactory.GetService<Movie>();

        // GET: Cart
        public ActionResult Index()
        {
            List<Movie> movies = new ShoppingCart(this.HttpContext).GetMoviesInCart();
            double total = 0;

            foreach (var movie in movies)
                total += movie.Price;

            CartViewModel model = new CartViewModel()
            {
                Movies = movies,
                Total = total
            };
            return View(model);
        }

        


        [ActionName("Add")]
        public ActionResult AddMovie(string prevUrl, int id)
        {
            if (!IsRented(id))
            {
                Movie m = movieManager.ReadOne(id);
                if (m != null)
                {
                    new ShoppingCart(this.HttpContext).AddMovieToCart(m);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return Redirect(prevUrl);
        }

        public ActionResult Delete(int id)
        {
            new ShoppingCart(this.HttpContext).DeleteFromCart(id);
            return RedirectToAction("Index");
        }

        public bool IsRented(int id)
        {
            if (new ShoppingCart(this.HttpContext).GetMoviesInCart().FirstOrDefault(x => x.Id == id) != null)
            {
                return true;
            }
            return false;
        }
    }
}