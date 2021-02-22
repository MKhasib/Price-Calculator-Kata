using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    class Program
    {
        static void Main(string[] args)
        {

            Product product = new Product ("The Little Prince", 12345, 20.25,new Cap ( 1F,true));
            //Product product = new Product("The Little Prince", 12345, 20.25, new Cap(4, false));

            //Product product = new Product("The Little Prince", 12345, 20.25, new Cap(.3, true));
            product.setUPCDiscount(12345, 0.07f);

            product.UniversalDiscountPercantage = .15f;
               var expenses = new List<Expense>
            { 
           new Expense("transport",.03,true)
            };
             
             Product.TaxPercantage = .21f;
            product.AddExpense(expenses[0]);
            
            product.printReport();
            Console.ReadLine();
        }
    }
}
