using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public class EventNoArg {
        List<CallbackNoArg> callbacks = new List<CallbackNoArg>();

        public void Clear() {
            callbacks.Clear();
        }
        public void Add(Method callback) {
            callbacks.Add(new CallbackNoArg(callback));
        }
        public void Fire() {
            foreach (CallbackNoArg callback in callbacks) {
                callback.Execute();
            }
        }
    }

    public class EventOneArg<P> {
        List<CallbackOneArg<P>> callbacks = new List<CallbackOneArg<P>>();

        public void Clear() {
            callbacks.Clear();
        }

        public void Add(Method<P> method) {
            callbacks.Add(new CallbackOneArg<P>(method));
        }

        public void Fire(P p) {
            foreach (CallbackOneArg<P> callback in callbacks) {
                callback.Execute(p);
            }
        }
    }

    public class EventTwoArgs<P, Q> {
        List<CallbackTwoArgs<P, Q>> callbacks = new List<CallbackTwoArgs<P, Q>>();

        public void Clear() {
            callbacks.Clear();
        }

        public void Add(Method<P, Q> method) {
            callbacks.Add(new CallbackTwoArgs<P, Q>(method));
        }

        public void Fire(P p, Q q) {
            foreach (CallbackTwoArgs<P, Q> callback in callbacks) {
                callback.Execute(p, q);
            }
        }
    }
}
