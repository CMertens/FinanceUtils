using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceUtils.Monies;

namespace FinanceUtils.Finance {
    public static class SimpleReturnUtility {
        public static Money GetSimpleReturn(Money StartAmt, Money EndAmt, Money Dividend) {
            Money CalcReturn = new Money(StartAmt.BaseCurrency.IsoCode);
            CalcReturn.BaseAmount = (((EndAmt.BaseAmount - StartAmt.BaseAmount) + Dividend.BaseAmount) / StartAmt.BaseAmount);
            return (CalcReturn);
        }

        public static Decimal GetEffectiveRate(Decimal NominalRate, int cPeriods) {
            return (((1 + (NominalRate / Convert.ToDecimal(cPeriods))) - 1));
        }

        public static Decimal GetContinuousRate(Decimal NominalRate, int Years) {
            return Convert.ToDecimal((Math.Pow(Math.E, (Convert.ToDouble(NominalRate * Years)) - 1)));
        }

        // Unverified; see TODO below.
        public static Money GetCompoundedMoney(Money Deposit, Decimal NominalRate, int cPeriods) {
            Decimal EffRate = GetEffectiveRate(NominalRate, cPeriods);
            Money ret = new Money(Deposit.BaseCurrency.IsoCode);
            // TODO: Test to see if this is working properly. Depends on whether PrecisionRounding() is working correctly in Money
            ret = ret * (1 + EffRate);
            return (ret);
        }

        public static Money GetCompoundedMoney(Money Deposit, Decimal NominalRate, int cPeriods, int Years) {
            double b1 = Convert.ToDouble((1 + (NominalRate / cPeriods)));
            double b2 = (cPeriods * Years);
            // TODO: Check to see if RegionInfo is set properly in returned Money value
            Money ret = (Deposit * Convert.ToDecimal(Math.Pow(b1, b2)));
            return (ret);
        }

    }
}
