using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public abstract class UIMessageAnswer : UIContainer{
        public void GiveAnswer(bool answer) {
            OnAnswer.Fire(answer);
            answerHandler(answer);
            Hide();
        }

        public abstract void Show(Method<bool> answer);

        public abstract void Hide();

        protected Method<bool> answerHandler;

        public EventOneArg<bool> OnAnswer;
    }
}
