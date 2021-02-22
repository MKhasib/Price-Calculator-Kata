using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class Expense
    { public String Description { get; private set; }
      public double Amount { get; private set; }
      public bool IsPercantage { get; private set; }
       public Expense(String description,double amount,bool isPercantage)
        {
            this.Description = description;
            this.Amount = amount;
            this.IsPercantage = isPercantage;
        }
    }
}
