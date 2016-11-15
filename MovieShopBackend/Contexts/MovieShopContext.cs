using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace MovieShopBackend.Contexts
{
    public class MovieShopInitializer : DropCreateDatabaseAlways<MovieShopContext>
    {
        
        protected override void Seed(MovieShopContext context)
        {
            IList<Genre> genres = new List<Genre>();
            IList<Movie> movies =new List<Movie>();
            
            genres.Add(new Genre
            {
                Name = "Action"
            });
            genres.Add(new Genre
            {
                Name = "DickJokes"
            });
            genres.Add(new Genre
            {
                Name = "Dokumentary"
            });
            genres.Add(new Genre
            {
                Name = "Horror"
            });


            movies.Add(new Movie
            {
               
                GenreId = 1,               
                ImageUrl = "http://i.imgur.com/Fh6xlbF.jpg",
                Title = "A Smashing Experience",
                Trailer = "https://www.youtube.com/embed/MQqFuGMCaT4",
                Price = 100000,
                Year = 2016

            });

            movies.Add(new Movie
            {
               
                GenreId = 2,                
                ImageUrl = "http://images2.fanpop.com/images/photos/7500000/Legolas-the-elves-of-middle-earth-7510893-477-406.jpg",
                Title = "Isengard Tour Guid!",
                Trailer = "https://www.youtube.com/embed/uE-1RPDqJAY",
                Price = 100000,
                Year = 2016
            });
            movies.Add( new Movie
            {
                GenreId = 3,
                ImageUrl = "https://i1.sndcdn.com/avatars-000174643992-6l0gyv-t500x500.jpg",
                Title = "A Normaly day in Japan",
                Trailer = "https://www.youtube.com/embed/GKqWButuYk0",
                Price = 999999,
                Year = 2016
            });
            movies.Add(new Movie
            {
                GenreId = 4,
                ImageUrl = "http://s1.ibtimes.com/sites/www.ibtimes.com/files/styles/large/public/2015/09/11/mama-june.jpg",
                Title = "An Amarican Story",
                Trailer = "https://www.youtube.com/embed/xmd1b2Lf6yU",
                Price = 0.001,
                Year = 2014

            });

            foreach (Genre genre in genres)
            {
                context.Set<Genre>().Add(genre);
                
            }
            foreach (Movie movie in movies)
            {
                context.Set<Movie>().Add(movie);
            }

            

            base.Seed(context);
        }
    }
    public class MovieShopContext : DbContext
    {
        public MovieShopContext() : base()
        {
            Database.SetInitializer<MovieShopContext>(new MovieShopInitializer());
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Create one-to-one relationship between Customer and Address.
            modelBuilder.Entity<Address>()
                .HasKey(address => address.Id);

            modelBuilder.Entity<Customer>()
                .HasRequired(customer => customer.Address)
                .WithRequiredPrincipal(address => address.Customer)
                .WillCascadeOnDelete(true);

            //Create one-to-many relationship between Order and Customer.
            modelBuilder.Entity<Order>()
                .HasRequired<Customer>(order => order.Customer)
                .WithMany(customer => customer.Order)
                .HasForeignKey(order => order.CustomerId)
                .WillCascadeOnDelete(true);

            //Create one-to-many relationship between Movie and Genre.
            modelBuilder.Entity<Movie>()
                .HasRequired<Genre>(movie => movie.Genre)
                .WithMany(genre => genre.Movies)
                .HasForeignKey(movie => movie.GenreId)
                .WillCascadeOnDelete(true);

            //Create many-to-many relationship between Movie and Orders.
            modelBuilder.Entity<Movie>()
                .HasMany<Order>(movie => movie.Orders)
                .WithMany(order => order.Movies)
                .Map(movieAndOrder =>
                {
                    movieAndOrder.MapLeftKey("MovieRefId");
                    movieAndOrder.MapRightKey("OrderRefId");
                    movieAndOrder.ToTable("MovieAndOrder");
                });

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Creating and naming the tubles that wil be awailable in the database
        /// </summary>
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
