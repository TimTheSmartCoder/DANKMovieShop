using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;

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
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomersCreateViewModel, MovieShopBackend.Entities.Customer>();
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomersCreateViewModel, MovieShopBackend.Entities.Address>();

                config.CreateMap<MovieShopAdmin.Models.Customers.CustomerEditViewModel, MovieShopBackend.Entities.Customer>();
                config.CreateMap<MovieShopAdmin.Models.Customers.CustomerEditViewModel, MovieShopBackend.Entities.Address>();
                config.CreateMap<MovieShopBackend.Entities.Customer, MovieShopAdmin.Models.Customers.CustomerEditViewModel>();
                config.CreateMap<MovieShopBackend.Entities.Address, MovieShopAdmin.Models.Customers.CustomerEditViewModel>();
                //Customer mapping end.

                //Genres mapping begin.
                config.CreateMap<MovieShopAdmin.Models.Genres.GenresCreateViewModel, MovieShopBackend.Entities.Genre>();
                config.CreateMap<MovieShopAdmin.Models.Genres.GenresEditViewModel, MovieShopBackend.Entities.Genre>();
                config.CreateMap<MovieShopBackend.Entities.Genre, MovieShopAdmin.Models.Genres.GenresCreateViewModel>();
                config.CreateMap<MovieShopBackend.Entities.Genre, MovieShopAdmin.Models.Genres.GenresEditViewModel>();
                //Genres mapping end.

                //Movies mapping begin.
                config.CreateMap<MovieShopAdmin.Models.Movies.MoviesCreateViewModel, MovieShopBackend.Entities.Movie>();
                config.CreateMap<MovieShopAdmin.Models.Movies.MoviesEditViewModel, MovieShopBackend.Entities.Movie>();
                config.CreateMap<MovieShopBackend.Entities.Movie, MovieShopAdmin.Models.Movies.MoviesCreateViewModel>();
                config.CreateMap<MovieShopBackend.Entities.Movie, MovieShopAdmin.Models.Movies.MoviesEditViewModel>();
                //Movies mapping end.
            });
            //SETUP OFF AUTOMAPPER TO ONLY LOAD MAPPING INFORMATION ONE TIME - END.
        }
    }
}
