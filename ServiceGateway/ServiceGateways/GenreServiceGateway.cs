using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceGateway.ServiceGateways
{
    internal class GenreServiceGateway : AbstractServiceGateway<Genre>
    {
        protected override Uri GetRestApiUri()
        {
            return new Uri("http://localhost:54202/api/Genres");
        }
    }
}
