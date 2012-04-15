using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FinanceUtils.Monies {
    public class IncompatibleCurrencyException : Exception {
        public IncompatibleCurrencyException() { }
        public IncompatibleCurrencyException(String s) : base(s) { }
        public IncompatibleCurrencyException(SerializationInfo i, StreamingContext s) : base(i, s) { }
        public IncompatibleCurrencyException(String s, Exception ie) : base(s, ie) { }
    }
}
