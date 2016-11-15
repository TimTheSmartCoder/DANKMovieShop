using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceGateway
{
    public interface ICurrencyRateServiceGateway : IServiceGateway<CurrencyRate>
    {
        CurrencyRate GetCurrencyRate(string from);
    }
}
