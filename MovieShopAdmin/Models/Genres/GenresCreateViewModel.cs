using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShopAdmin.Models.Genres
{
    public class GenresCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}