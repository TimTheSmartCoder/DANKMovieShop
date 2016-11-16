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
        private CurrencyManager _currencyManager;

        private List<Movie> movies = new List<Movie>();
        private List<Genre> genres = new List<Genre>();
        private CurrencyRate currencyRate = new CurrencyRate();

        public ShopController()
        {      
            this._currencyManager = new CurrencyManager(System.Web.HttpContext.Current, _currencyRateManager);
                 
            movies = _movieManager.ReadAll();
            genres = _GenreManager.ReadAll();
            currencyRate = _currencyRateManager.GetCurrencyRate("DKK");

            //Convert price to the correct currency.
            foreach (Movie movie in movies)
                movie.Price = this._currencyManager.Convert(movie.Price);    
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
                CurrencyName = _currencyManager.GetSelectedCurrency()

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
                    Movies = movies.FindAll(x => x.GenreId == genre.Id),
                    SelectedGenre = genre,
                    CurrencyRate = currencyRate,
                    CurrencyName = _currencyManager.GetSelectedCurrency() 
                };
                return View("Index", viewModel);
            
        }

        [HttpGet]
        public ActionResult Currency(string currency, string returnUrl)
        {
            if (currency != null)
                this._currencyManager.SetCurrency(currency);
            
            return Redirect(returnUrl);
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