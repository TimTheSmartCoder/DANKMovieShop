using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGateway.Exceptions
{
    [Serializable]
    public class ServiceGatewayException : Exception
    {
        public ServiceGatewayException(string message) 
            : base(message)
        {
            //Nothing to do.
        }

        public ServiceGatewayException(string message, Exception innerException) 
            : base(message, innerException)
        {
            //Nothing to do.
        }
    }
}
