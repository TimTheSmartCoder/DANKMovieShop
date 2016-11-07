using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;

namespace MovieShopBackend.Managers
{
    class GenreManager : AbstractManager<Genre>
    {
        protected override List<Genre> ReadAll(MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Genre>()
                .Include(x => x.Movies)
                .ToList();
        }

        protected override Genre ReadOne(int id, MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Genre>()
                .Include(x => x.Movies)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
