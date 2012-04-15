using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace FinanceUtils.Monies {
    public class NoSuchCurrencyException : Exception {
        public NoSuchCurrencyException() { }
        public NoSuchCurrencyException(String s) : base(s) { }
        public NoSuchCurrencyException(SerializationInfo i, StreamingContext s) : base(i, s) { }
        public NoSuchCurrencyException(String s, Exception ie) : base(s, ie) { }
    }
}
