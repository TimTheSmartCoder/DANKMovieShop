using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using MovieShopBackend;
using MovieShopUser.Models;
using ServiceGateway;

namespace MovieShopUser.Controllers
{
    public class ShopController : Controller
    {
        private IServiceGateway<Movie> _movieManager = ServiceGatewayFactory.GetService<Movie>();
        private IServiceGateway<Genre> _GenreManager = ServiceGatewayFactory.GetService<Genre>();
        private ICurrencyRateServiceGateway _currencyRateManager = ServiceGatewayFactory.GetService<CurrencyRate, ICurrencyRateServiceGateway>();

        private List<Movie> movies = new List<Movie>();
        private List<Genre> genres = new List<Genre>();
        private CurrencyRate currencyRate = new CurrencyRate();

        public ShopController()
        {           
            movies = _movieManager.ReadAll();
            genres = _GenreManager.ReadAll();
            currencyRate = _currencyRateManager.GetCurrencyRate("DKK");            
        }

        

        [HttpGet]
        public ActionResult Index()
        {
            GenreMovieRateViewModel viewModel = new GenreMovieRateViewModel
            {
                Genres = genres,
                Movies = movies,
                SelectedGenre = new Genre
                {
                    Id = -1,
                    Name = "All"
                },
                CurrencyRate = currencyRate,              
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Filtert(int id)
        {
            
            
            Genre genre = _GenreManager.ReadOne(id);
            

            if (genre == null)
                {
                    return HttpNotFound();
                }
            
                GenreMovieRateViewModel viewModel = new GenreMovieRateViewModel
                {
                    Genres = _GenreManager.ReadAll(),
                    Movies = _movieManager.ReadAll().FindAll(x => x.GenreId == genre.Id),
                    SelectedGenre = genre,
                    CurrencyRate = currencyRate,                   
                };
                return View("Index", viewModel);
            
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            Movie movie = _movieManager.ReadOne(id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }
        
    }
}