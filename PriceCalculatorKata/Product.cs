using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;

namespace PriceCalculatorKata
{   enum DiscountType
    {
        Universal=1,
        UPC=2
    }
    
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

        private static Discount UniversalDiscount = new Discount();
        public double UniversalDiscountPercantage
        {
            get { return UniversalDiscount.DiscountPercantage; }
            set
            {
                UniversalDiscount.DiscountPercantage = value;
                printDiscount();
            }
        }
        private Discount UPCDiscount = new Discount();
        public void setUPCDiscount(int UPCCode,double discountPercantage)
        {
            if (this.UPCCode == UPCCode)
             UPCDiscount.DiscountPercantage = discountPercantage;
            
        }
        public void setDiscountTime(int type,bool isBeforeTaxation)
        {
            switch (type)
            {
                case (int)DiscountType.Universal: UniversalDiscount.IsAppliedBeforeTaxation = isBeforeTaxation;
                    break;
                case (int)DiscountType.UPC:
                    UPCDiscount.IsAppliedBeforeTaxation = isBeforeTaxation;
                    break;
            }
        }
        public void printDiscount()
        {
            double discountPercantageBeforeTaxation = getDiscountPercantageBeforeTaxation(UPCDiscount, UniversalDiscount);
            double discountAmountBeforeTaxation = roundNumberToTwoDecimals(calculatePriceAmount(Price, discountPercantageBeforeTaxation));
            double taxAmount = roundNumberToTwoDecimals(calculatePriceAmount(Price-discountAmountBeforeTaxation, TaxPercantage));
            double discountPercantageAfterTaxation = getDiscountPercantageAfterTaxation(UPCDiscount, UniversalDiscount);
            double discountAmountAfterTaxation = calculatePriceAmount(Price- discountAmountBeforeTaxation, discountPercantageAfterTaxation);
            double finalPrice = (Price + taxAmount - (discountAmountAfterTaxation + discountAmountBeforeTaxation));
            Console.WriteLine($"Price:{convertNumberToStringCurrency(roundNumberToTwoDecimals(finalPrice))}");
            Console.WriteLine($"{convertNumberToStringCurrency(roundNumberToTwoDecimals(discountAmountAfterTaxation + discountAmountBeforeTaxation))} amount which was deduced");
        }

   
        private double getDiscountPercantageBeforeTaxation(Discount UPCDiscount, Discount relativeDiscount)
        {
            double totalPercantage = 0;
            if (UPCDiscount.IsAppliedBeforeTaxation)
                totalPercantage += UPCDiscount.DiscountPercantage;
            if(relativeDiscount.IsAppliedBeforeTaxation)
                totalPercantage += relativeDiscount.DiscountPercantage;
            return totalPercantage;
        }
        private double getDiscountPercantageAfterTaxation(Discount uPCDiscount, Discount relativeDiscount)
        {
            double totalPercantage = 0;
            if (!UPCDiscount.IsAppliedBeforeTaxation)
                totalPercantage += UPCDiscount.DiscountPercantage;
            if (!relativeDiscount.IsAppliedBeforeTaxation)
                totalPercantage += relativeDiscount.DiscountPercantage;
            return totalPercantage;
        }
        //public void getReport()
        //{
        //    double priceWithTax = calculatePriceWithTax(Price, TaxPercantage);
        //    Console.WriteLine($"Product price reported as {convertNumberToStringCurrency(roundNumberToTwoDecimals(Price))} " +
        //        $"before tax and {convertNumberToStringCurrency(priceWithTax)} " +
        //        $"after {roundNumberToTwoDecimals(TaxPercantage * 100)}% tax");

        //    double taxAmount = calculatePriceAmount(Price, TaxPercantage);
        //    double discountAmount = calculatePriceAmount(Price, totalDiscount);
        //    String taxStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(taxAmount));
        //    String discountStringAmount = convertNumberToStringCurrency(roundNumberToTwoDecimals(discountAmount));

        //    Console.WriteLine($"Tax = {roundNumberToTwoDecimals(TaxPercantage * 100)}%," +
        //        $" discount = {roundNumberToTwoDecimals(totalDiscount * 100)}%" +
        //        $" Tax amount = {taxStringAmount}; Discount amount = {discountStringAmount}");
        //    double finalPrice = (Price + taxAmount - discountAmount);
        //    Console.WriteLine($"Price before = {convertNumberToStringCurrency(roundNumberToTwoDecimals(Price))}," +
        //        $" price after = {convertNumberToStringCurrency(roundNumberToTwoDecimals(finalPrice))}");
        //}
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
