using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {

    public delegate void Method();
    public delegate void Method<P>(P p);
    public delegate void Method<P, Q>(P p, Q q);

    public class CallbackTwoArgs<P, Q> {
        public CallbackTwoArgs(Method<P, Q> method) {
            this.method = method;
        }
        public void Execute(P p, Q q) {
            method(p, q);
        }
        private Method<P, Q> method;
    }

    public class CallbackOneArg<P> {
        public CallbackOneArg(Method<P> method) {
            this.method = method;
        }
        public void Execute(P parameter) {
            method(parameter);
        }
        private Method<P> method;
    }

    public class CallbackNoArg {
        public CallbackNoArg(Method method) {
            this.method = method;
        }
        public void Execute() {
            method();
        }
        private Method method;
    }
}
