using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopBackend.Entities
{
    public class Genre : AbstractEntity
    {
        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
