using System;

namespace ServiceGateway.Exceptions
{
    [Serializable]
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message)
            : base(message)
        {
            //Nothing to do.
        }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
            //Nothing to do.
        }
    }
}
