using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace MovieShopUser.Models
{
    public class GenreMovieViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Genre> Genres { get; set; }
        public Movie Movie { get; set; }
        public Genre SelectedGenre { get; set; }

    }
}