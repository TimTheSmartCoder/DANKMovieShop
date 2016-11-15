using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Rate
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class CurrencyRate : AbstractEntity
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public List<Rate> Rates { get; set; }

    }
}
