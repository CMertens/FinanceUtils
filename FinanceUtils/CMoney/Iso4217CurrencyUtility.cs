using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;


namespace FinanceUtils.Monies {
    /* Class: Iso4217CurrencyUtility
     * Basic class to find ISO 4217 currency information.
     * Throws: fViU_NoSuchCurrencyException
     */
    public static class Iso4217CurrencyUtility {
        /* Find a RegionInfo via a three-letter ISO 4217 code */
        public static RegionInfo GetRegionByIso4217Code(String IsoCode) {
            RegionInfo regionInfo = (from c in CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
                                     let r = new RegionInfo(c.LCID)
                                     where r.ISOCurrencySymbol == IsoCode
                                     select r).First();
            if (regionInfo == null) {
                throw new NoSuchCurrencyException("No such currency: " + IsoCode);
            } else {
                return (regionInfo);
            }
        }

        // Load fractional data for currencies.
        private static List<CurrencyFractionalDetail> LoadHardCodedFractionals() {
            List<CurrencyFractionalDetail> cfList = new List<CurrencyFractionalDetail>();
            cfList.Add(new CurrencyFractionalDetail() { Code = "AED", FractionName = "Fils", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AFN", FractionName = "Pul", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ALL", FractionName = "Qintar", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AMD", FractionName = "Luma", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ANG", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AOA", FractionName = "Cêntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ARS", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AUD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AWG", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "AZN", FractionName = "Q?pik", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BAM", FractionName = "Fening", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BBD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BDT", FractionName = "Paisa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BGN", FractionName = "Stotinka", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BHD", FractionName = "Fils", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BIF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BMD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BND", FractionName = "Sen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BOB", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BRL", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BSD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BTN", FractionName = "Chertrum", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BWP", FractionName = "Thebe", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BYR", FractionName = "Kapyeyka", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "BZD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CAD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CDF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CHF", FractionName = "Rappen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CLP", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CNY", FractionName = "Jiao", FractionToBase = 10 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "COP", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CRC", FractionName = "Céntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CUC", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CUP", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CVE", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "CZK", FractionName = "Halér", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "DJF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "DKK", FractionName = "Øre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "DOP", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "DZD", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "EEK", FractionName = "Sent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "EGP", FractionName = "Piastre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ERN", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ETB", FractionName = "Santim", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "EUR", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "FJD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "FKP", FractionName = "Penny", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GBP", FractionName = "Penny", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GEL", FractionName = "Tetri", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GHS", FractionName = "Pesewa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GIP", FractionName = "Penny", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GMD", FractionName = "Butut", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GNF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GTQ", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "GYD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "HKD", FractionName = "Ho", FractionToBase = 10 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "HNL", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "HRK", FractionName = "Lipa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "HTG", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "HUF", FractionName = "Fillér", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "IDR", FractionName = "Sen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ILS", FractionName = "Agora", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "INR", FractionName = "Paisa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "IQD", FractionName = "Fils", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "IRR", FractionName = "Dinar", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ISK", FractionName = "Eyrir", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "JMD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "JOD", FractionName = "Piastre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "JPY", FractionName = "Sen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KES", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KGS", FractionName = "Tyiyn", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KHR", FractionName = "Sen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KMF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KPW", FractionName = "Chon", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KRW", FractionName = "Jeon", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KWD", FractionName = "Fils", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KYD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "KZT", FractionName = "Tiyn", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LAK", FractionName = "Att", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LBP", FractionName = "Piastre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LKR", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LRD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LSL", FractionName = "Sente", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LTL", FractionName = "Centas", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LVL", FractionName = "Santims", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "LYD", FractionName = "Dirham", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MAD", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MDL", FractionName = "Ban", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MGA", FractionName = "Iraimbilanja", FractionToBase = 5 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MKD", FractionName = "Deni", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MMK", FractionName = "Pya", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MNT", FractionName = "Möngö", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MOP", FractionName = "Avo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MRO", FractionName = "Khoums", FractionToBase = 5 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MUR", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MVR", FractionName = "Laari", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MWK", FractionName = "Tambala", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MXN", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MYR", FractionName = "Sen", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "MZN", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NAD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NGN", FractionName = "Kobo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NIO", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NOK", FractionName = "Øre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NPR", FractionName = "Paisa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "NZD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "OMR", FractionName = "Baisa", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PAB", FractionName = "Centésimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PEN", FractionName = "Céntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PGK", FractionName = "Toea", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PHP", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PKR", FractionName = "Paisa", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PLN", FractionName = "Grosz", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "PYG", FractionName = "Céntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "QAR", FractionName = "Dirham", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "RON", FractionName = "Ban", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "RSD", FractionName = "Para", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "RUB", FractionName = "Kopek", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "RWF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SAR", FractionName = "Hallallah", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SBD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SCR", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SDG", FractionName = "Piastre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SEK", FractionName = "Öre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SGD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SHP", FractionName = "Penny", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SKK", FractionName = "Halier", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SLL", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SOS", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SRD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "STD", FractionName = "Cêntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SVC", FractionName = "Centavo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SYP", FractionName = "Piastre", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "SZL", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "THB", FractionName = "Satang", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TJS", FractionName = "Diram", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TMM", FractionName = "Tennesi", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TND", FractionName = "Millime", FractionToBase = 1000 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TOP", FractionName = "Seniti", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TRY", FractionName = "New kurus", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TTD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TWD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "TZS", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "UAH", FractionName = "Kopiyka", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "UGX", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "USD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "UYU", FractionName = "Centésimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "UZS", FractionName = "Tiyin", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "VEF", FractionName = "Céntimo", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "VND", FractionName = "Hào", FractionToBase = 10 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "WST", FractionName = "Sene", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "XAF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "XCD", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "XOF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "XPF", FractionName = "Centime", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "YER", FractionName = "Fils", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ZAR", FractionName = "Cent", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ZMK", FractionName = "Ngwee", FractionToBase = 100 });
            cfList.Add(new CurrencyFractionalDetail() { Code = "ZWD", FractionName = "Cent", FractionToBase = 100 });
            return (cfList);
        }

        // Get fractional data for a specified currency. Note that all Money is stored, at base
        // in fractional currency and then divided to create a Decimal representation.
        public static CurrencyFractionalDetail GetCoreFractionByIso4217Code(String IsoCode) {
            RegionInfo regionInfo = Iso4217CurrencyUtility.GetRegionByIso4217Code(IsoCode);
            CurrencyFractionalDetail cfdet = (from cf in LoadHardCodedFractionals()
                                              where cf.Code == IsoCode
                                              select cf).First();
            if (cfdet.Code == null) {
                throw new Exception("Could not find fractional for currency: " + IsoCode);
            }
            return (cfdet);
        }
    }
}
