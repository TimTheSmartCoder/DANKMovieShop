using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Entities;
using ServiceGateway.Exceptions;

namespace ServiceGateway.ServiceGateways
{
    internal abstract class AbstractServiceGateway<T> : IServiceGateway<T> where T : AbstractEntity
    {
        protected readonly HttpClient Client;

        protected AbstractServiceGateway()
        {
            //Create client.
            this.Client = new HttpClient();
            this.Client.BaseAddress = this.GetHost();
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public T Create(T entity)
        {
            HttpResponseMessage response = this.Client.PostAsJsonAsync(GetRestApiUri().AbsolutePath, entity).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().Result;

            throw new ServiceGatewayException(
                $"Failed to create {entity.GetType().Name} with {GetRestApiUri().AbsoluteUri}");
        }

        public List<T> ReadAll()
        {
            HttpResponseMessage response = this.Client.GetAsync(GetRestApiUri().AbsolutePath).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<T>>().Result;

            throw new ServiceGatewayException(
                $"Failed to get all the entites of {typeof(T).Name} with {GetRestApiUri().AbsoluteUri}");
        }

        public T ReadOne(int id)
        {
            HttpResponseMessage response = this.Client.GetAsync($"{GetRestApiUri().AbsolutePath}/{id}").Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().Result;

            throw new ServiceGatewayException(
                $"Failed to get the entity {typeof(T).Name} with {GetRestApiUri().AbsoluteUri}");
        }

        public T Update(T entity)
        {
            HttpResponseMessage response =
                this.Client.PostAsJsonAsync($"{GetRestApiUri().AbsolutePath}/{entity.Id}", entity).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<T>().Result;

            throw new ServiceGatewayException(
                $"Failed to update the entity {typeof(T).Name} with {GetRestApiUri().AbsoluteUri}");
        }

        public bool Delete(int id)
        {
            HttpResponseMessage response = this.Client.DeleteAsync($"{GetRestApiUri().AbsolutePath}/{id}").Result;

            if (response.IsSuccessStatusCode)
                return true;

            throw new ServiceGatewayException(
                $"Failed to delete the entity {typeof(T).Name} with {GetRestApiUri().AbsoluteUri}");
        }

        protected abstract Uri GetRestApiUri();

        /// <summary>
        /// Gets the host and the port in from the rest api uri.
        /// </summary>
        /// <returns></returns>
        private Uri GetHost()
        {
            return new Uri($"http://{this.GetRestApiUri().Host}:{this.GetRestApiUri().Port}");
        }
    }
}
