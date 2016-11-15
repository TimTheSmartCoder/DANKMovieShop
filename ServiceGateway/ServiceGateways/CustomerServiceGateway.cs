using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Authentication;

namespace ServiceGateway.ServiceGateways
{
    class CustomerServiceGateway : AbstractServiceGateway<Customer>
    {
        public CustomerServiceGateway() : base(new HttpAuthentication("restapi", "DANKMovieShop2016", new Uri("http://localhost:54202/token")))
        {

        }

        protected override Uri GetRestApiUri()
        {
            return new Uri("http://localhost:54202/api/Customers");
        }
    }
}
