using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopBackend;
using MovieShopBackend.Entities;
using MovieShopUser.Models;

namespace MovieShopUser.Controllers
{
    public class ShopController : Controller
    {
        private IManager<Movie> _movieManager = new ManagerFacade().GetMovieManager();
        private IManager<Genre> _GenreManager = new ManagerFacade().GetGenreManager();

        private List<Movie> movies = new List<Movie>();
        private List<Genre> genres = new List<Genre>();

        public ShopController()
        {           
            movies = _movieManager.ReadAll();
            genres = _GenreManager.ReadAll();
        }

        

        [HttpGet]
        public ActionResult Index()
        {
            GenreMovieViewModel viewModel = new GenreMovieViewModel
            {
                Genres = genres,
                Movies = movies,
                SelectedGenre = new Genre
                {
                    Id = -1,
                    Name = "All"
                }

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

            GenreMovieViewModel viewModel = new GenreMovieViewModel
            {
                Genres = _GenreManager.ReadAll(),
                Movies = genre.Movies,
                SelectedGenre = genre
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