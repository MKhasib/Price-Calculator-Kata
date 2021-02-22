using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculatorKata
{
    public class Currency
    {
        public static bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
        {
            symbol = CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
                .Select(ri => ri.CurrencySymbol)
                .FirstOrDefault();
            return symbol != null;
        }
        public static string getCurrencySymbolFromISO(String ISOCode)
        {
            string currnecySymbol;
            if (TryGetCurrencySymbol(ISOCode, out currnecySymbol))
                return currnecySymbol;
            return ISOCode;



        }
    }
}
