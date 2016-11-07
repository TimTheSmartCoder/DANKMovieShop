using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Entities;

namespace MovieShopAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //SETUP OFF AUTOMAPPER TO ONLY LOAD MAPPING INFORMATION ONE TIME - BEGIN.
            Mapper.Initialize(config =>
            {
                //Customer mapping begin.
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomersCreateViewModel, Entities.Customer>();
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomersCreateViewModel, Entities.Address>();

                config.CreateMap<MovieShopAdmin.Models.Customers.CustomerEditViewModel, Entities.Customer>();
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomerEditViewModel, Entities.Address>();
                config.CreateMap<Entities.Customer, MovieShopAdmin.Models.Customers.CustomerEditViewModel>();
                config.CreateMap<Entities.Address, MovieShopAdmin.Models.Customers.CustomerEditViewModel>();
                //Customer mapping end.

                //Genres mapping begin.
                config.CreateMap<MovieShopAdmin.Models.Genres.GenresCreateViewModel, Entities.Genre>();
                config.CreateMap<MovieShopAdmin.Models.Genres.GenresEditViewModel, Entities.Genre>();
                config.CreateMap<Entities.Genre, MovieShopAdmin.Models.Genres.GenresCreateViewModel>();
                config.CreateMap<Entities.Genre, MovieShopAdmin.Models.Genres.GenresEditViewModel>();
                //Genres mapping end.

                //Movies mapping begin.
                config.CreateMap<MovieShopAdmin.Models.Movies.MoviesCreateViewModel, Entities.Movie>();
                config.CreateMap<MovieShopAdmin.Models.Movies.MoviesEditViewModel, Entities.Movie>();
                config.CreateMap<Entities.Movie, MovieShopAdmin.Models.Movies.MoviesCreateViewModel>();
                config.CreateMap<Entities.Movie, MovieShopAdmin.Models.Movies.MoviesEditViewModel>();
                //Movies mapping end.
            });
            //SETUP OFF AUTOMAPPER TO ONLY LOAD MAPPING INFORMATION ONE TIME - END.
        }
    }
}
