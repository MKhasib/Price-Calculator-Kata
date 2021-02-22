using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;

namespace PriceCalculatorKata
{
    enum DiscountType
    {
        Universal = 1,
        UPC = 2
    }

    public class Product
    {
        public Product() { }
        public Product(String name, int UPCCode, double price,Cap cap)
        {
            this.Name = name;
            this.UPCCode = UPCCode;
            this.Price = price;
            this.cap = cap;
        }
        public String Name { get; set; }
        public int UPCCode { get; private set; }
        public double Price { get; set; }
        public bool CombiningIsMultiplicative { get; set; } = true;
        public Cap cap { get; private set; }
        public static double TaxPercantage { get; set; } = 0.20F;

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
        public void setUPCDiscount(int UPCCode, double discountPercantage)
        {
            if (this.UPCCode == UPCCode)
                UPCDiscount.DiscountPercantage = discountPercantage;

        }
        public void setDiscountTime(int type, bool isBeforeTaxation)
        {
            switch (type)
            {
                case (int)DiscountType.Universal:
                    UniversalDiscount.IsAppliedBeforeTaxation = isBeforeTaxation;
                    break;
                case (int)DiscountType.UPC:
                    UPCDiscount.IsAppliedBeforeTaxation = isBeforeTaxation;
                    break;
            }
        }
        public void printDiscount()
        {
            double price = Price;
            var discount = calcaulteDiscount(price);
            var capAmount = cap.IsPercantage ? cap.Value * price : cap.Value;
            if (discount > capAmount)
                discount = capAmount;
            Console.WriteLine($"{convertNumberToStringCurrency(roundNumberToTwoDecimals(discount))} amount which was deduced");
        }

        

        private List<Expense> expenses = new List<Expense>();
        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
        }
        public void printReport()
        {
            var price = Price;
            var discountBeforeTaxation = calculateDiscounBeforeTaxation(price);
            if(CombiningIsMultiplicative)
            price -= discountBeforeTaxation;

            Console.WriteLine($"Cost = {convertNumberToStringCurrency( roundNumberToTwoDecimals(this.Price))}");
            var capAmount = cap.IsPercantage ? cap.Value * price : cap.Value;
            if (discountBeforeTaxation > capAmount)
            {
                discountBeforeTaxation = capAmount;
            }
            double taxAmount = roundNumberToTwoDecimals(calculatePriceAmount(Price - discountBeforeTaxation, TaxPercantage));
            var discountAfterTaxation = calculateDiscounAfterTaxation(price);
            double discount = roundNumberToTwoDecimals(discountBeforeTaxation + discountAfterTaxation);
            if (discount > capAmount)
                discount = capAmount;
            Console.WriteLine($"Tax = {convertNumberToStringCurrency(roundNumberToTwoDecimals(taxAmount))}");
            Console.WriteLine($"Discounts = {convertNumberToStringCurrency(roundNumberToTwoDecimals(discount))}");
            double extraCosts = 0;
            foreach(var expense in expenses)
            { var cost = 0.0;
                if (expense.IsPercantage)
                    cost = (expense.Amount * Price);
                else
                    cost = expense.Amount;
                Console.WriteLine($"{expense.Description} = {convertNumberToStringCurrency(roundNumberToTwoDecimals(cost))}");
                extraCosts += cost;
            }
            Console.WriteLine($"TOTAL = {convertNumberToStringCurrency(roundNumberToTwoDecimals(Price + taxAmount - discount + extraCosts))}");
        }
        private double calcaulteDiscount(double price)
        {
            var discountBeforeTaxation = calculateDiscounBeforeTaxation(price);
            if(CombiningIsMultiplicative)
            price -= discountBeforeTaxation;
            var discountAfterTaxation = calculateDiscounAfterTaxation(price);

            return roundNumberToTwoDecimals(discountBeforeTaxation+ discountAfterTaxation);
        }

        private double calculateDiscounBeforeTaxation(double price)
        {
            var UnverstialDiscountAmountBeforeTaxation = getUnverstialDiscountAmountBeforeTaxation(price);
            if (CombiningIsMultiplicative)
                price -= UnverstialDiscountAmountBeforeTaxation;
            var UPCDiscountAmountBeforeTaxation = getUPCDiscountAmountBeforeTaxation(price);
            return roundNumberToTwoDecimals(UnverstialDiscountAmountBeforeTaxation + UPCDiscountAmountBeforeTaxation);
        }
        private double calculateDiscounAfterTaxation(double price)
        {
            var UnverstialDiscountAmountAfterTaxation = getUnverstialDiscountAmountAfterTaxation(price);
            if (CombiningIsMultiplicative)
                price -= UnverstialDiscountAmountAfterTaxation;
            var UPCDiscountAmountAfterTaxation = getUPCDiscountAmountAfterTaxation(price);
            return roundNumberToTwoDecimals(UnverstialDiscountAmountAfterTaxation + UPCDiscountAmountAfterTaxation);
        }
        private double getUPCDiscountAmountBeforeTaxation(double price)
        {
            if (UPCDiscount.IsAppliedBeforeTaxation)
                return roundNumberToTwoDecimals(UPCDiscount.DiscountPercantage * price);
            return 0;
        }

        private double getUnverstialDiscountAmountBeforeTaxation(double price)
        {
            if (UniversalDiscount.IsAppliedBeforeTaxation)
                return roundNumberToTwoDecimals(UniversalDiscount.DiscountPercantage * price);
            return 0;
        }
        private double getUPCDiscountAmountAfterTaxation(double price)
        {
            if (!UPCDiscount.IsAppliedBeforeTaxation)
                return roundNumberToTwoDecimals(UPCDiscount.DiscountPercantage * price);
            return 0;
        }

        private double getUnverstialDiscountAmountAfterTaxation(double price)
        {
            if (!UniversalDiscount.IsAppliedBeforeTaxation)
                return roundNumberToTwoDecimals(UniversalDiscount.DiscountPercantage * price);
            return 0;
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
