using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public abstract class UIOverlay : UIRoot {
        public bool Showing() {
            return showing;
        }

        bool showing = false;

        public virtual void Show(Method<int> answer) {
            this.answerHandler = new CallbackOneArg<int>(answer);
            showing = true;
        }

        public virtual void GiveAnswer(int option) {
            showing = false;
            answerHandler.Execute(option);
        }

        protected CallbackOneArg<int> answerHandler;
    }
}
