using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;

namespace PriceCalculatorKata
{
   public class Product
    {
        public Product() { }
       public Product(String name,int UPCCode,double price)
        {
            this.Name = name;
            this.UPCCode = UPCCode;
            this.price = price;
        }
        public String Name { get; set; }
        public int UPCCode { get; private set; }
        public double taxPercantage { get; set; } = 0.20F;
        public double price { get; set; }
       
        public void getReport()
        {
            double priceWithTax = calculatePriceWithTax(price, taxPercantage);

            Console.WriteLine( $"Product price reported as {price} before tax and {convertNumberToStringCurrency(priceWithTax)} after {roundNumberToTwoDecimals(taxPercantage * 100)}% tax");
        }
        private double calculatePriceWithTax(double price, double tax)
        {

            return roundNumberToTwoDecimals((price + (price * taxPercantage)));
        }
        private double roundNumberToTwoDecimals(double number)
        {
            return (double)Math.Round(number, 2);
        }
        private String convertNumberToStringCurrency(double price)
        {
            return price.ToString("C", CultureInfo.GetCultureInfo("en-US"));
        }
        
      
    }
}
