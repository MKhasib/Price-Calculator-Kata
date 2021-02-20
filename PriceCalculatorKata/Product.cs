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
        public Product(String name, int UPCCode, double price)
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
            Console.WriteLine($"Product price reported as {convertNumberToStringCurrency(roundNumberToTwoDecimals(price))} " +
                $"before tax and {convertNumberToStringCurrency(priceWithTax)} " +
                $"after {roundNumberToTwoDecimals(taxPercantage * 100)}% tax");

            double taxAmount = calculatePriceAmount(price, taxPercantage);
            double discountAmount = calculatePriceAmount(price, Discount.discountPercantage);
            String taxStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(taxAmount));
            String discountStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(discountAmount));
            
            Console.WriteLine($"Tax={roundNumberToTwoDecimals(taxPercantage * 100)}%," +
                $"discount={roundNumberToTwoDecimals(Discount.discountPercantage * 100)}%" +
                $"Tax amount = {taxStringAmount}; Discount amount = {discountStringAmount}");
            double finalPrice = (price + taxAmount - discountAmount);
            Console.WriteLine($"Price before = {convertNumberToStringCurrency(roundNumberToTwoDecimals(price))}," +
                $" price after = {convertNumberToStringCurrency(roundNumberToTwoDecimals(finalPrice))}");
        }
        private double calculatePriceWithTax(double price, double tax)
        {

            return roundNumberToTwoDecimals((price + calculatePriceAmount(price, tax)));
        }

        private double calculatePriceAmount(double price, double percantage)
        {
            return (price * percantage);
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
