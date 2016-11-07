using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceGateway.ServiceGateways
{
    class OrderServiceGateway : AbstractServiceGateway<Order>
    {
        protected override Uri GetRestApiUri()
        {
            return new Uri("http://localhost:54202/api/Orders");
        }
    }
}
