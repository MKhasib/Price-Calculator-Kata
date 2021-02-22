using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
   public class Cap
    {
        public  double Value { get; set; }
        public  bool IsPercantage { get; set; }
        public Cap(double value,bool isPercantage)
        {
            this.Value = value;
            IsPercantage = isPercantage;
        }
    }
}
