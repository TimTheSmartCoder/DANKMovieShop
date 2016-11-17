using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceGateway;

namespace UnitTestProject
{
    [TestClass]
    public class CurrencyUnitTest
    {
        //tests if we have made the entity
        [TestMethod]
        public void NullTests()
        {
            ICurrencyRateServiceGateway _currencyRateManager = ServiceGatewayFactory.GetService<CurrencyRate, ICurrencyRateServiceGateway>();
            CurrencyRate currencyRate = new CurrencyRate();
            currencyRate = _currencyRateManager.GetCurrencyRate("DKK");

            Assert.IsNotNull(currencyRate);
            Assert.IsNotNull(currencyRate.Rates);
            
        }
        //tests is its the correst currencerate base.
        [TestMethod]
        public void CorrectTest()
        {
            ICurrencyRateServiceGateway _currencyRateManager = ServiceGatewayFactory.GetService<CurrencyRate, ICurrencyRateServiceGateway>();
            CurrencyRate currencyRate = new CurrencyRate();
            currencyRate = _currencyRateManager.GetCurrencyRate("DKK");

            Assert.AreEqual(currencyRate.Base, "DKK");           
        }

        //tests if the common currencyes are available in the list
        [TestMethod]
        public void SuportTest()
        {
            ICurrencyRateServiceGateway _currencyRateManager = ServiceGatewayFactory.GetService<CurrencyRate, ICurrencyRateServiceGateway>();
            CurrencyRate currencyRate = new CurrencyRate();
            currencyRate = _currencyRateManager.GetCurrencyRate("DKK");

            List<string> CommonCurrency = new List<string>();
            CommonCurrency.Add("EUR");
            CommonCurrency.Add("SEK");
            CommonCurrency.Add("USD");
            CommonCurrency.Add("GBP");


            foreach (string s in CommonCurrency)
            {
                Rate rate = currencyRate.Rates.FirstOrDefault(x => x.Name == s);
                Assert.AreEqual(rate.Name, s);
            }
        }
}
}
