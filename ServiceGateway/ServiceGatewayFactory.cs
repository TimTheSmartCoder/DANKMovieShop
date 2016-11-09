using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Exceptions;
using ServiceGateway.ServiceGateways;

namespace ServiceGateway
{
    public class ServiceGatewayFactory
    {
        /// <summary>
        /// Get the Service Gateway for the given type.
        /// </summary>
        /// <typeparam name="T">Entity to get ServiceGateway for.</typeparam>
        /// <returns></returns>
        public static IServiceGateway<T> GetService<T>() where T : AbstractEntity
        {
            return GetService<T, IServiceGateway<T>>();
        }

        /// <summary>
        /// Gets the ServiceGateway for the given type and returns the given
        /// Interface.
        /// </summary>
        /// <typeparam name="T">Entity to get ServiceGateway for.</typeparam>
        /// <typeparam name="TK">Interface type to return.</typeparam>
        /// <returns></returns>
        public static TK GetService<T, TK>() where T : AbstractEntity where TK : IServiceGateway<T>
        {
            //Namespace to the Service Gateway classes.
            string @namespace = "ServiceGateway.ServiceGateways";

            //Queries all of the Service Gateways classes except the Abstract.
            IEnumerable<Type> classes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                        where t.IsClass && t.Namespace == @namespace && !t.IsAbstract
                                        select t;

            foreach (Type @class in classes)
                if (@class != null && @class.BaseType.GetGenericArguments()[0].Name == typeof(T).Name)
                    return (TK)Activator.CreateInstance(@class);


            throw new ServiceGatewayException($"Failed to find a gateway for {typeof(T).Name}");
        }
    }
}
