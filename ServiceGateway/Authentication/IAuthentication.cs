using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Authentication
{
    internal interface IAuthentication
    {
        /// <summary>
        /// This method will try to log the user in and
        /// if it succes it will return a HttpClient
        /// and it fails return null.
        /// </summary>
        ///<param name="client">Client which to login.</param>
        /// <returns></returns>
        HttpClient Login(HttpClient client);
    }
}
