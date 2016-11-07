using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;

namespace MovieShopBackend.Managers
{
    class AddressManager : AbstractManager<Address>
    {
        protected override List<Address> ReadAll(MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Address>().ToList();
        }

        protected override Address ReadOne(int id, MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Address>().FirstOrDefault(address => address.Id == id);
        }
    }
}
