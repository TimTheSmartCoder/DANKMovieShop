using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MovieShopBackend.Managers;

namespace MovieShopBackend
{
    public class ManagerFacade
    {
        public IManager<Genre> GetGenreManager()
        {
            return new GenreManager();
        }

        public IManager<Customer> GetCustomerManager()
        {
            return new CustomerManager();
        }

        public IManager<Address> GetAddressManager()
        {
            return new AddressManager();
        }

        public IManager<Movie> GetMovieManager()
        {
            return new MovieManager();
        }

        public IManager<Order> GetOrderManager()
        {
            return new OrderManager();
        }
    }
}
