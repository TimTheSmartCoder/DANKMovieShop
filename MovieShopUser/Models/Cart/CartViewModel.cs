using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopBackend.Entities;

namespace MovieShopUser.Models.Cart
{
    public class CartViewModel
    {
        private List<Movie> _movies;

        public double Total { get; set; }

        public List<Movie> Movies
        {
            get { return _movies; }
            set
            {
                if (value != null)
                {
                    _movies = value;
                }
                else
                {
                    throw new ArgumentException("Movies can not be null");
                }
            }
        }
    }
}