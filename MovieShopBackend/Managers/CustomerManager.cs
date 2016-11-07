using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;

namespace MovieShopBackend.Managers
{
    class CustomerManager : AbstractManager<Customer>
    {
        protected override List<Customer> ReadAll(MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Customer>()
                .Include(x => x.Order)
                .Include(x => x.Address)
                .ToList();
        }

        protected override Customer ReadOne(int id, MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Customer>()
                .Include(x => x.Order)
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
