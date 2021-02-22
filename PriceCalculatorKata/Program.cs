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
            Product product = new Product ("The Little Prince", 12345, 20.25);
            //{
            //    product.setUPCDiscount(12345, 0.07F);
            //    //product.setUPCDiscount(12344, 0.07F);
            //    product.UniversalDiscountPercantage = .15F;
            //    var expenses = new List<Expense>
            //{ new Expense("Packaging",0.01F,true),
            //new Expense("Transport",2.2,false),
            //};
            //    foreach (var expense in expenses)
            //    {
            //        product.AddExpense(expense);
            //    }
            //    Product.TaxPercantage = .21F;
            //}


            //product.setUPCDiscount(12345, 0.07F);
            //product.setUPCDiscount(12344, 0.07F);
            //product.UniversalDiscountPercantage = .15F;
            var expenses = new List<Expense>
            { new Expense("Packaging",0.01F,true),
            new Expense("Transport",2.2,false),
            };
            //foreach(var expense in expenses)
            //{
            //    product.AddExpense(expense);
            //}
            Product.TaxPercantage = .21F;
            product.printReport();
            Console.ReadLine();
            product.printReport();
            Console.ReadLine();
        }
    }
}
