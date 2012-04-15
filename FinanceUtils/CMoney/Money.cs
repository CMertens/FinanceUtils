using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceUtils.Monies {
    // disable warnings about not overriding GetHashCode()
#pragma warning disable 0659,0661
    /* The Money struct is designed to be a high-precision, globalized representation of money.
     * It is suitable for simple in-currency manipulations as well as international transactions when used with the CurrencyExchange classes.
     * Note that because Money uses an Int64 internally, it can handle negative values but may not be suitable for extremely large
     * (several quadrillion) currency values. This may limit usefulness in hyperinflationary scenarios. If this becomes an issue, then
     * rewriting the class to use a UInt64 plus a negative flag is possible.
     */
    // TODO: Exchange Rate methods
    public struct Money : IComparable, IComparable<Money>, IEquatable<Money> {

        /*
         * Truncate: Round to zero (drop fractional amount) ALWAYS
         * Simple: Round up if .5 or greater; down if .4 or less
         * AwayFromZero: Round up to next value ALWAYS
         * ToEven: If .5, round to nearest even number
         * Statistical: If .5, round up or down on 50/50 probability split
         * Argentine: if third digit is less than 3, change to 0 or drop. If third digit is greater than 2 and less than 8, change to 5. If third digit is greater than 7, add 1 to second digit and change third digit to 0 or drop.
         * Swiss: If last two digits are less than 26, change to 0. If last two digits are greater than 75, add one to the digit above and drop the two digits. If the last two digits are greater than 25 and less than 76, change them to '5'.
         */
        public enum RoundingType {
            Truncate,
            Simple,
            AwayFromZero,
            ToEven,
            Statistical,
            Argentine,
            Swiss,
            None
        }

        /* Variables and Properties */
        #region Variables and Properties
        private Currency p_CurrencyType;
        private Int64 p_BaseAmount;
        public RoundingType RoundingFlag;

        public decimal Amount {
            get { return (Convert.ToDecimal(p_BaseAmount) / Convert.ToDecimal(p_CurrencyType.FractionalToBase)); }
            set { p_BaseAmount = Convert.ToInt64((Convert.ToDecimal(value) * Convert.ToDecimal(p_CurrencyType.FractionalToBase))); }
        }
        public Int64 BaseAmount {
            get { return (p_BaseAmount); }
            set { p_BaseAmount = value; }
        }

        public Currency BaseCurrency {
            get { return (this.p_CurrencyType); }
        }
        #endregion

        /* Constructors */
        #region Constructors
        public Money(String s) {
            p_CurrencyType = new Currency(s);
            p_BaseAmount = 0;
            if (p_CurrencyType.GetCulture().Name == "CH") {
                RoundingFlag = RoundingType.Swiss;
            } else if (p_CurrencyType.GetCulture().Name == "AR") {
                RoundingFlag = RoundingType.Argentine;
            } else {
                RoundingFlag = RoundingType.Simple;
            }
        }
        public Money(String s, Int64 i) {
            p_CurrencyType = new Currency(s);
            p_BaseAmount = i;
            if (p_CurrencyType.GetCulture().Name == "CH") {
                RoundingFlag = RoundingType.Swiss;
            } else if (p_CurrencyType.GetCulture().Name == "AR") {
                RoundingFlag = RoundingType.Argentine;
            } else {
                RoundingFlag = RoundingType.Simple;
            }
        }
        public Money(String s, Int32 i) {
            p_CurrencyType = new Currency(s);
            p_BaseAmount = i;
            if (p_CurrencyType.GetCulture().Name == "CH") {
                RoundingFlag = RoundingType.Swiss;
            } else if (p_CurrencyType.GetCulture().Name == "AR") {
                RoundingFlag = RoundingType.Argentine;
            } else {
                RoundingFlag = RoundingType.Simple;
            }
        }
        public Money(String s, Decimal d) {
            p_CurrencyType = new Currency(s);
            p_BaseAmount = 0;
            if (p_CurrencyType.GetCulture().Name == "CH") {
                RoundingFlag = RoundingType.Swiss;
            } else if (p_CurrencyType.GetCulture().Name == "AR") {
                RoundingFlag = RoundingType.Argentine;
            } else {
                RoundingFlag = RoundingType.Simple;
            }
            Amount = d;
        }
        #endregion

        /* Operator Overloads */
        #region Operator Overloads
        // TODO: Add methods that take ints. However, should these interact with the fractional or decimalized representations of the underlying data?
        public static Money operator +(Money m1, decimal d) {
            Int64 i = Convert.ToInt64(d * m1.p_CurrencyType.FractionalToBase);
            m1.p_BaseAmount = m1.p_BaseAmount + i;
            return (m1);
        }
        public static Money operator +(Money m1, Money m2) {
            if (m1.p_CurrencyType.Symbol != m2.p_CurrencyType.Symbol) {
                throw new IncompatibleCurrencyException("Tried to convert from currency " + m1.p_CurrencyType.Symbol + " to " + m2.p_CurrencyType.Symbol);
            }
            m1.p_BaseAmount = m1.p_BaseAmount + m2.p_BaseAmount;
            return (m1);
        }
        public static Money operator -(Money m1, decimal d) {
            Int64 i = Convert.ToInt64(d * m1.p_CurrencyType.FractionalToBase);
            m1.p_BaseAmount = m1.p_BaseAmount - i;
            return (m1);
        }
        public static Money operator -(Money m1, Money m2) {
            if (m1.p_CurrencyType.Symbol != m2.p_CurrencyType.Symbol) {
                throw new IncompatibleCurrencyException("Tried to convert from currency " + m1.p_CurrencyType.Symbol + " to " + m2.p_CurrencyType.Symbol);
            }
            m1.p_BaseAmount = m1.p_BaseAmount - m2.p_BaseAmount;
            return (m1);
        }
        public static Money operator *(Money m1, int i) {
            m1.p_BaseAmount = m1.p_BaseAmount * i;
            return (m1);
        }
        public static Money operator *(Money m1, decimal d) {
            m1.p_BaseAmount = m1.PrecisionRounding(m1.BaseAmount * d);
            return (m1);
        }
        public static Money operator /(Money m1, int i) {
            m1.p_BaseAmount = m1.p_BaseAmount / i;
            return (m1);
        }
        public static Money operator /(Money m1, decimal d) {
            m1.p_BaseAmount = m1.PrecisionRounding(m1.BaseAmount / d);
            return (m1);
        }
        public static Money operator ^(Money m1, int i) {
            m1.p_BaseAmount = m1.p_BaseAmount ^ i;
            return (m1);
        }
        public static bool operator ==(Money m1, decimal d) {
            Int64 i = Convert.ToInt64(d * m1.p_CurrencyType.FractionalToBase);
            return (m1.p_BaseAmount == i);
        }
        public static bool operator ==(Money m1, Money m2) {
            if (m1.p_CurrencyType.Symbol != m2.p_CurrencyType.Symbol) {
                throw new IncompatibleCurrencyException("Tried to convert from currency " + m1.p_CurrencyType.Symbol + " to " + m2.p_CurrencyType.Symbol);
            }
            return (m1.p_BaseAmount == m2.p_BaseAmount);
        }
        public static bool operator !=(Money m1, decimal d) {
            Int64 i = Convert.ToInt64(d * m1.p_CurrencyType.FractionalToBase);
            return (!(m1.p_BaseAmount == i));
        }
        public static bool operator !=(Money m1, Money m2) {
            if (m1.p_CurrencyType.Symbol != m2.p_CurrencyType.Symbol) {
                throw new IncompatibleCurrencyException("Tried to convert from currency " + m1.p_CurrencyType.Symbol + " to " + m2.p_CurrencyType.Symbol);
            }
            return (!(m1.p_BaseAmount == m2.p_BaseAmount));
        }
        #endregion

        /* Method Overrides */
        #region Method Overrides
        public bool Equals(Money m1) {
            return (this.p_BaseAmount == m1.p_BaseAmount && this.p_CurrencyType.Symbol == m1.p_CurrencyType.Symbol);
        }
        public bool Equals(decimal d) {
            return (this.p_BaseAmount == (Convert.ToInt64(this.p_CurrencyType.FractionalToBase * d)));
        }
        public override bool Equals(object o) {
            return base.Equals(o);
        }


        // TODO: this is broken, due to impendence mismatch between CultureInfo and RegionInfo names
        /*
        public override string ToString() {
            CultureInfo ci = new CultureInfo(this.p_CurrencyType.GetCulture,false);
            NumberFormatInfo nfi = ci.NumberFormat;
            return(this.Amount.ToString("c",nfi));
        }
        */

        public int CompareTo(Object o) {
            throw new Exception("Object is " + o.GetType().ToString() + " and is not supported in CompareTo() operations!");
        }
        public int CompareTo(Money m1) {
            if (m1.p_CurrencyType.Symbol != this.p_CurrencyType.Symbol) {
                throw new IncompatibleCurrencyException("Tried to convert from currency " + this.p_CurrencyType.Symbol + " to " + m1.p_CurrencyType.Symbol);
            }
            if (this.p_BaseAmount == m1.p_BaseAmount) {
                return (0);
            } else if (this.p_BaseAmount > m1.p_BaseAmount) {
                return (1);
            } else if (this.p_BaseAmount < m1.p_BaseAmount) {
                return (-1);
            } else {
                throw new Exception("Impossible comparison between Money values " + this.p_BaseAmount + " and " + m1.p_BaseAmount);
            }
        }
        public int CompareTo(decimal d) {
            Int64 i = Convert.ToInt64(d * this.p_CurrencyType.FractionalToBase);
            if (this.p_BaseAmount == i) {
                return (0);
            } else if (this.p_BaseAmount > i) {
                return (1);
            } else if (this.p_BaseAmount < i) {
                return (-1);
            } else {
                throw new Exception("Impossible comparison between Money and Decimal values " + this.p_BaseAmount + " and " + d + " (" + i + ")");
            }
        }
        #endregion

        #region Methods

        /*
         * Allocation algorithm: allocate the money value across n Bins, or n Bins with n allocation ratios
         * Algorithm from Martin Fowler
         */
        public Money[] Allocate(int Bins) {
            Money[] binarr = new Money[Bins];
            Int64 principal = this.p_BaseAmount / Bins;
            Int64 remainder = (this.p_BaseAmount % principal);
            for (int x = 0; x < binarr.Length; x++) {
                binarr[x] = new Money(this.p_CurrencyType.GetCulture().ISOCurrencySymbol);
                binarr[x].p_BaseAmount = principal;
            }
            int y = 0;
            while (remainder > 0) {
                binarr[y].p_BaseAmount++;
                remainder--;
                if (y == (binarr.Length - 1)) {
                    y = 0;
                } else {
                    y++;
                }
            }
            return (binarr);
        }

        public Money[] Allocate(int[] RatioBins) {
            throw new NotImplementedException();
        }


        /*
         * Truncate: Round towards zero (drop fractional amount) ALWAYS
         * Simple: Round up if .5 or greater; down if .4 or less
         * AwayFromZero: Round up to next value ALWAYS
         * ToEven: If .5, round to nearest even number
         * Statistical: If .5, round up or down on 50/50 probability split
         * Argentine: if third digit is less than 3, change to 0 or drop. If third digit is greater than 2 and less than 8, change to 5. If third digit is greater than 7, add 1 to second digit and change third digit to 0 or drop.
         * Swiss: If last two digits are less than 26, change to 0. If last two digits are greater than 75, add one to the digit above and drop the two digits. If the last two digits are greater than 25 and less than 76, change them to '5'.
         */
        private Int64 PrecisionRounding(decimal d) {
            bool Negative = false;
            decimal t = Decimal.Truncate(d);
            decimal i = d - t;
            if (i < 0) {
                Negative = true;
                i = i * -1;
            }

            String integralString = i.ToString();
            int Precision = integralString.Length - 2;

            // create inverted array of chars from integralString
            char[] carr = new char[Precision];
            for (int x = 0; x < Precision; x++) {
                carr[x] = integralString[Precision + 1 - x];
            }
            switch (this.RoundingFlag) {
                case RoundingType.Argentine:
                    // TODO
                    return (-1);
                case RoundingType.AwayFromZero:
                    if (Negative) {
                        return (Int64)((t + 1) * -1);
                    } else {
                        return (Int64)(t + 1);
                    }
                case RoundingType.Simple:
                    Int64 riafz = (Int64)Math.Round(d, 0, MidpointRounding.AwayFromZero);
                    if (Negative) {
                        riafz = riafz * -1;
                    }
                    return (riafz);
                case RoundingType.Statistical:
                    Random rand = new Random();
                    for (int fy = 0; fy < (carr.Length - 1); fy++) {
                        if (Convert.ToInt64(carr[fy]) > 5) {
                            carr[fy + 1]++;
                        } else { // randomly perturb next value
                            if (rand.Next() % 2 == 0) {
                                carr[fy + 1]++;
                            }
                        }
                        carr[fy] = '0';
                    }
                    if (carr[carr.Length - 1] > 5) {
                        t++;
                    } else {
                        if (rand.Next() % 2 == 0) {
                            t++;
                        }
                    }
                    return Convert.ToInt64(t);
                case RoundingType.Swiss:
                    // TODO
                    return (-1);
                case RoundingType.ToEven:
                    Int64 rite = (Int64)Math.Round(d, 0, MidpointRounding.ToEven);
                    if (Negative) {
                        rite = rite * -1;
                    }
                    return (rite);
                case RoundingType.Truncate:
                    return (Int64)(t);
                default:
                    throw new Exception("Unknown RoundingFlag (" + this.RoundingFlag + ") in PrecisionRounding()");
            }
        }
        #endregion


    }
}
