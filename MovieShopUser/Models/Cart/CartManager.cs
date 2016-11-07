using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace MovieShopUser.Models.Cart
{
    public class CartManager
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();

        public Movie Add(Movie m)
        {
            if (m != null)
            {
                Movies.Add(m);
            }
            return m;
        }

        public void Delete(int id)
        {
            Movies.RemoveAll(x => x.Id == id);
        }
    }
}