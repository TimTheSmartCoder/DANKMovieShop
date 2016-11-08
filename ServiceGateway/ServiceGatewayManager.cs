using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            //Namespace to the Service Gateway classes.
            string @namespace = "ServiceGateway.ServiceGateways";

            //Queries all of the Service Gateways classes except the Abstract.
            IEnumerable<Type> classes = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.Namespace == @namespace && !t.IsAbstract
                select t;

            foreach (Type @class in classes)
                if (@class != null && @class.BaseType.GetGenericArguments()[0].Name == typeof(T).Name)
                    return (IServiceGateway<T>) Activator.CreateInstance(@class);


            throw new ServiceGatewayException($"Failed to find a gateway for {typeof(T).Name}");
        }
    }
}
