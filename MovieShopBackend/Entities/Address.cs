using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopBackend.Entities
{
    public class Address : AbstractEntity
    {
        public Customer Customer { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        public int ZipCode { get; set; }

        public string Country { get; set; }
    }
}

