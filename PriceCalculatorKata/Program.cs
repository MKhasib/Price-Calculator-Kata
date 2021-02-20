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
            product.getReport();
            product.setUPCDiscount(12345, 0.07F);
            //product.setUPCDiscount(12344, 0.07F);
            product.RelativeDiscountPercantage = .15F;
            Console.ReadLine();
        }
    }
}
