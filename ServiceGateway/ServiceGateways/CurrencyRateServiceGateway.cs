using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl.Runtime;
using Entities;
using Newtonsoft.Json.Linq;
using ServiceGateway.Exceptions;

namespace ServiceGateway.ServiceGateways
{
    internal class CurrencyRateServiceGateway : AbstractServiceGateway<CurrencyRate> , ICurrencyRateServiceGateway
    {
        

        protected override Uri GetRestApiUri()
        {
            return new Uri("http://api.fixer.io/latest");
        }

        public CurrencyRate GetCurrencyRate(string @from)
        {
            HttpResponseMessage response = this.Client.GetAsync($"{GetRestApiUri().AbsolutePath}?base={@from}").Result;

            if (response.IsSuccessStatusCode)
            {
                var str = response.Content.ReadAsStringAsync().Result;
                JObject root = JObject.Parse(str);
                JObject rates = root.Value<JObject>("rates");
                DateTime date = root.Value<DateTime>("date");

                CurrencyRate currencyRate = new CurrencyRate
                {
                    Base = @from,
                    Date = date,
                    Rates = new List<Rate>()
                };

                foreach (var rate in rates)
                {
                    double value = 0;
                    Double.TryParse(rate.Value.ToString(), out value);

                    currencyRate.Rates.Add(new Rate()
                    {
                        Name = rate.Key,
                        Value = value
                    });
                }
                return currencyRate;

            }
              
            throw new ServiceGatewayException(
                $"Failed to get the entity {typeof(CurrencyRate).Name} with {GetRestApiUri().AbsoluteUri}");
        }
    }
}
