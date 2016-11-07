using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MovieShopBackend.Contexts;
using MovieShopBackend.Entities;
using System.Data.Entity;

namespace MovieShopBackend.Managers
{
    class MovieManager : AbstractManager<Movie>
    {
        public override Movie Create(Movie movie)
        {
            using (MovieShopContext context = new MovieShopContext())
            {
                if (movie != null && movie.Orders != null)
                    foreach (var order in movie.Orders)
                        context.Set<Order>().Attach(order);

                if (movie.Genre != null)
                    context.Set<Genre>().Attach(movie.Genre);

                context.Set<Movie>().Add(movie);
                context.SaveChanges();

                return movie;
            }
        }

        protected override List<Movie> ReadAll(MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Movie>()
                .Include(x => x.Genre)
                .Include(x => x.Orders)
                .ToList();
        }

        protected override Movie ReadOne(int id, MovieShopContext movieShopContext)
        {
            return movieShopContext.Set<Movie>()
                .Include(x => x.Genre)
                .Include(x => x.Orders)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
