using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Exceptions;
using ServiceGateway.ServiceGateways;

namespace ServiceGateway
{
    public class ServiceGatewayManager
    {
        public static IServiceGateway<T> GetService<T>() where T : AbstractEntity
        {
            string name = typeof(T).Name;

            if (name.Equals(typeof(Address).Name))
                return (IServiceGateway<T>) new AddressServiceGateway();

            if (name.Equals(typeof(Customer).Name))
                return (IServiceGateway<T>) new CustomerServiceGateway();

            if (name.Equals(typeof(Genre).Name))
                return (IServiceGateway<T>) new GenreServiceGateway();

            if (name.Equals(typeof(Movie).Name))
                return (IServiceGateway<T>) new MovieServiceGateway();

            if (name.Equals(typeof(Order).Name))
                return (IServiceGateway<T>) new OrderServiceGateway();

            throw new ServiceGatewayException($"Failed to find a gateway for {typeof(T).Name}");
        }
    }
}
