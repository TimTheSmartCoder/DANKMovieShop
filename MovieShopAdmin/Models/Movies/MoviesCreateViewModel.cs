using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieShopAdmin.Models.Movies
{
    public class MoviesCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0, 3000)]
        public int Year { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        public string Trailer { get; set; }

        [Required]
        public int GenreId { get; set; }

        public SelectList Genres { get; set; }
    }
}