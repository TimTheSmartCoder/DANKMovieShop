using System.Collections.Generic;

namespace Entities
{
    public class Customer : AbstractEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }

        public string Email { get; set; }

        public List<Order> Order { get; set; }
    }
}