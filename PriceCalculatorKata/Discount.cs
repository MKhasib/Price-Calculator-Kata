using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
   
    class Discount
    {   
        public double DiscountPercantage { get; set; } = 0F;
        public bool IsAppliedBeforeTaxation { get; set; } = false;
    }
}
