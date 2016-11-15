using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using ServiceGateway;

namespace MovieShopUser.Models
{
    internal class CurrencyManager
    {
        public CurrencyManager(HttpContext httpContext, ICurrencyRateServiceGateway currencyRateService)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));
            if (currencyRateService == null)
                throw new ArgumentNullException(nameof(currencyRateService));

            this._httpContext = httpContext;
            this._currencyRateService = currencyRateService;

            if (httpContext.Request.Cookies.Get(CurrencyCookieName) != null)
            {
                //Get the currency.
                string currency = httpContext.Request.Cookies.Get(CurrencyCookieName).Value;

                //Get the rates for the base currency.
                CurrencyRate currencyRate = this._currencyRateService.GetCurrencyRate(BaseCurrency);

                if (currencyRate.Rates.FirstOrDefault(r => r.Name == currency) != null)
                {
                    this.rate = currencyRate.Rates.FirstOrDefault(r => r.Name == currency);
                }
            }
        }

        private const string CurrencyCookieName = "CurrencyCookieName";
        private const string BaseCurrency = "DKK";
        private readonly HttpContext _httpContext;
        private readonly ICurrencyRateServiceGateway _currencyRateService;
        private Rate rate;

        public void SetCurrency(string currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));
            
            this._httpContext.Response.SetCookie(new HttpCookie(CurrencyCookieName, currency));

            //Get the rates for the base currency.
            CurrencyRate currencyRate = this._currencyRateService.GetCurrencyRate(BaseCurrency);

            if (currencyRate.Rates.FirstOrDefault(r => r.Name == currency) != null)
            {
                this.rate = currencyRate.Rates.FirstOrDefault(r => r.Name == currency);
            }
        }

        public double Convert(double value)
        {
            if (this.rate == null)
                return value;

            return value * this.rate.Value;
        }
    }
}