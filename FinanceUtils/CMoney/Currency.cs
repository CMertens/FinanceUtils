using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FinanceUtils.Monies {
    /* Base class for managing currency 
 * Contains fractional currency data, which all Money objects use under the hood
 */
    public struct Currency {

        private RegionInfo Region;
        private String p_FractionalName;
        private int p_FractionalToBase;
        private DateTime p_ValidOn;
        private DateTime p_ValidFrom;
        private DateTime p_ValidTo;

        String p_IsoCode;

        public String IsoCode { get { return (p_IsoCode); } set { p_IsoCode = value; } }

        public String Symbol {
            get { return (Region.CurrencySymbol); }
        }
        public String Name {
            get { return (Region.CurrencyEnglishName); }
        }
        public String NativeName {
            get { return (Region.CurrencyNativeName); }
        }

        public String FractionalName {
            get { return (p_FractionalName); }
        }
        public int FractionalToBase {
            get { return (p_FractionalToBase); }
        }
        public DateTime ValidOn {
            get { return (p_ValidOn); }
        }

        // from inclusive
        public DateTime ValidFrom {
            get { return (p_ValidFrom); }
        }
        // to inclusive
        public DateTime ValidTo {
            get { return (p_ValidTo); }
        }


        public Currency(String s) {
            p_IsoCode = s;
            Region = Iso4217CurrencyUtility.GetRegionByIso4217Code(s);
            p_FractionalName = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionName;
            p_FractionalToBase = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionToBase;
            p_ValidOn = DateTime.Now;
            p_ValidFrom = DateTime.MinValue;
            p_ValidTo = DateTime.MaxValue;
        }
        public Currency(String s, DateTime dt) {
            p_IsoCode = s;
            Region = Iso4217CurrencyUtility.GetRegionByIso4217Code(s);
            p_FractionalName = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionName;
            p_FractionalToBase = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionToBase;
            p_ValidOn = dt;
            p_ValidFrom = DateTime.MinValue;
            p_ValidTo = DateTime.MaxValue;
        }
        public Currency(String s, DateTime dt_on, DateTime dt_min, DateTime dt_max) {
            p_IsoCode = s;
            Region = Iso4217CurrencyUtility.GetRegionByIso4217Code(s);
            p_FractionalName = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionName;
            p_FractionalToBase = Iso4217CurrencyUtility.GetCoreFractionByIso4217Code(s).FractionToBase;
            p_ValidOn = dt_on;
            p_ValidFrom = dt_min;
            p_ValidTo = dt_max;
        }

        public RegionInfo GetCulture() {
            return (new RegionInfo(Region.TwoLetterISORegionName));
        }

    }
}
