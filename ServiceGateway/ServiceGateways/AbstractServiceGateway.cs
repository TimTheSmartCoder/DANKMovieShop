using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceGateway.Exceptions;

namespace ServiceGateway
{
    internal abstract class AbstractServiceGateway<T> : IServiceGateway<T> where T : AbstractEntity
    {
        public T Create(T entity)
        {
            using (HttpClient client = new HttpClient())
            {
                //Create client.
                client.BaseAddress = this.GetHost();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync(this.GetRestApiUri().AbsolutePath, entity).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<T>().Result;
                
                throw new ServiceGatewayException($"Failed to create {entity.GetType().Name} with {this.GetRestApiUri().AbsoluteUri}");
            }
        }

        public List<T> ReadAll()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.GetHost();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(this.GetRestApiUri().AbsolutePath).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<List<T>>().Result;

                throw new ServiceGatewayException($"Failed to get all the entites of {typeof(T).Name} with {this.GetRestApiUri().AbsoluteUri}");
            }
        }

        public T ReadOne(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.GetHost();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync($"{this.GetRestApiUri().AbsolutePath}/{id}").Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<T>().Result;

                throw new ServiceGatewayException($"Failed to get the entity {typeof(T).Name} with {this.GetRestApiUri().AbsoluteUri}");
            }
        }

        public T Update(T entity)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.GetHost();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync($"{this.GetRestApiUri().AbsolutePath}/{entity.Id}", entity).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<T>().Result;

                throw new ServiceGatewayException($"Failed to update the entity {typeof(T).Name} with {this.GetRestApiUri().AbsoluteUri}");
            }
        }

        public bool Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.GetHost();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync($"{this.GetRestApiUri().AbsolutePath}/{id}").Result;

                if (response.IsSuccessStatusCode)
                    return true;

                throw new ServiceGatewayException($"Failed to delete the entity {typeof(T).Name} with {this.GetRestApiUri().AbsoluteUri}");
            }
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
