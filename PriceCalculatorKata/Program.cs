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
            product.taxPercantage = .21;
            product.getReport();
            Console.ReadLine();
        }
    }
}
