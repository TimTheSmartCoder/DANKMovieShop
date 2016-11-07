using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopBackend.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// MovieShopBackendException exception for handling exception in the
        /// backend.
        /// </summary>
        /// <param name="message">Erorr message.</param>
        public EntityNotFoundException(string message) : base(message)
        {
            //No code is needed for the constructor.
        }
    }
}
