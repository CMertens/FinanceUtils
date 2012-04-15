using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FinanceUtils.Monies {
    public struct Iso4217CurrencyExchangeRate {
        public DateTime Time;
        public RegionInfo FromCurrencyRegion;
        public RegionInfo ToCurrencyRegion;
        public decimal ExchangeRate;
    }
}
