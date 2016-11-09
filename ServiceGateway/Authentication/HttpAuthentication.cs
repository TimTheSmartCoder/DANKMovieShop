using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ServiceGateway.Exceptions;

namespace ServiceGateway.Authentication
{
    internal class HttpAuthentication : AbstractAuthentication
    {
        private readonly string _username;
        private readonly string _password;
        private readonly Uri _authenticationApi;

        public HttpAuthentication(string username, string password, Uri authenticationApi)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (authenticationApi == null)
                throw new ArgumentNullException(nameof(authenticationApi));

            this._username = username;
            this._password = password;
            this._authenticationApi = authenticationApi;
        }

        public override HttpClient Login(HttpClient client)
        {
            //Get the token.
            string token = this.GetAuthenticationToken();

            //Add the header with the authentication token.
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return client;
        }

        private string GetAuthenticationToken()
        {
            using (HttpClient client = new HttpClient())
            {
                //Create HttpClient to authentication.
                client.BaseAddress = this._authenticationApi;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );

                //Create form content for login authentication.
                var formContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", this._username),
                    new KeyValuePair<string, string>("password", this._password)
                });

                HttpResponseMessage response = client.PostAsync("/token", formContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Get result.
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    //Parse json to json object.
                    var json = JObject.Parse(jsonResponse);

                    //Get token from json response.
                    return json.GetValue("access_token").ToString();
                }

                throw new AuthenticationException("Failed to login with the given cridentials.");
            }
        }
    }
}
