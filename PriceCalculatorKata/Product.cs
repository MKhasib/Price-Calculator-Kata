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
            this.Price = price;
        }
        public String Name { get; set; }
        public int UPCCode { get; private set; }
        public double Price { get; set; }
        public double TaxPercantage { get; set; } = 0.20F;
        private static double discountPercantage=0;
        public double DiscountPercantage
        {
            get { return discountPercantage; }
            set
            {
                discountPercantage = value;
                printDiscount();
            }
        } 

        private void printDiscount()
        {
            double taxAmount = calculatePriceAmount(Price, TaxPercantage);
            double discountAmount = calculatePriceAmount(Price, discountPercantage);
            double finalPrice = (Price + taxAmount - discountAmount);

            Console.WriteLine($"Price:{convertNumberToStringCurrency(roundNumberToTwoDecimals(finalPrice))}");
            Console.WriteLine($"{convertNumberToStringCurrency(roundNumberToTwoDecimals(discountAmount))} amount which was deduced");
        }

        public void getReport()
        {
            double priceWithTax = calculatePriceWithTax(Price, TaxPercantage);
            Console.WriteLine($"Product price reported as {convertNumberToStringCurrency(roundNumberToTwoDecimals(Price))} " +
                $"before tax and {convertNumberToStringCurrency(priceWithTax)} " +
                $"after {roundNumberToTwoDecimals(TaxPercantage * 100)}% tax");

            double taxAmount = calculatePriceAmount(Price, TaxPercantage);
            double discountAmount = calculatePriceAmount(Price, discountPercantage);
            String taxStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(taxAmount));
            String discountStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(discountAmount));

            Console.WriteLine($"Tax = {roundNumberToTwoDecimals(TaxPercantage * 100)}%," +
                $" discount = {roundNumberToTwoDecimals(discountPercantage * 100)}%" +
                $" Tax amount = {taxStringAmount}; Discount amount = {discountStringAmount}");
            double finalPrice = (Price + taxAmount - discountAmount);
            Console.WriteLine($"Price before = {convertNumberToStringCurrency(roundNumberToTwoDecimals(Price))}," +
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
