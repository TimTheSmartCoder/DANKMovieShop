using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopBackend.Entities;

namespace MovieShopUser.Models.Cart
{
    public class ShoppingCart
    {
        private string _key = "Movies";
        private HttpContextBase _context;

        public ShoppingCart(HttpContextBase context)
        {
            _context = context;
            if (context.Session[_key] == null)
            {
                context.Session[_key] = new List<Movie>();
            }
        }

        public List<Movie> GetMoviesInCart()
        {
            return (List<Movie>) _context.Session[_key];
        }

        public void AddMovieToCart(Movie m)
        {
            if (m != null)
            {
                ((List<Movie>) _context.Session[_key]).Add(m);
            }
        }

        public void ClearCart()
        {
            ((List<Movie>)_context.Session[_key]).Clear();
        }

        public void DeleteFromCart(int id)
        {
            ((List<Movie>) _context.Session[_key]).RemoveAll(x => x.Id == id);
        }
    }
}