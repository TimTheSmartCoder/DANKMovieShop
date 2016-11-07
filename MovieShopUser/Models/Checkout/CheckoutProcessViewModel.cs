using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShopUser.Models.Checkout
{
    public class CheckoutProcessViewModel
    {
        /// <summary>
        /// Customer Entity.
        /// </summary>

        [Required]
        public int Id { get; set; } = -1;

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Address Entity.
        /// </summary>

        [Required]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int ZipCode { get; set; }
    }
}