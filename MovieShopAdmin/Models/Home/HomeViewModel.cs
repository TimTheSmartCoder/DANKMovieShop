using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopBackend.Entities;

namespace MovieShopAdmin.Models.Home
{
    public class HomeViewModel
    {
        public List<Order> LastFiveOrders { get; set; }
        public int NumberOfOrders { get; set; }
        public int NumberOfCustomers { get; set; }
    }
}