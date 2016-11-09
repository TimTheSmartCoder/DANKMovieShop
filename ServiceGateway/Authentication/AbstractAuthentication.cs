using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Authentication
{
    internal abstract class AbstractAuthentication : IAuthentication
    {
        /// <summary>
        /// Constructs AbstractAuthentication.
        /// </summary>
        protected AbstractAuthentication()
        {
            //Nothing yet!!!
        }

        public abstract HttpClient Login(HttpClient client);
    }
}
