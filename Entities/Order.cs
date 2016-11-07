using System;
using System.Collections.Generic;

namespace Entities
{
    public class Order : AbstractEntity
    {
        public DateTime Date { get; set; }

        public List<Movie> Movies { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
