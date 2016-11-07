using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using MovieShopBackend.Contexts;

namespace MovieShopBackend.Managers
{
    class OrderManager : AbstractManager<Order>
    {
        public override Order Create(Order order)
        {
            using (MovieShopContext context = new MovieShopContext())
            {
                if (order != null && order.Movies != null)
                    foreach (var orderMovie in order.Movies)
                        context.Set<Movie>().Attach(orderMovie);
                
                context.Set<Order>().Add(order);
                context.SaveChanges();

                return order;
            }
        }

        protected override List<Order> ReadAll(MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Order>()
                .Include(x => x.Customer)
                .Include(x => x.Movies)
                .ToList();
        }

        protected override Order ReadOne(int id, MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Order>()
                .Include(x => x.Customer)
                .Include(x => x.Movies)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
