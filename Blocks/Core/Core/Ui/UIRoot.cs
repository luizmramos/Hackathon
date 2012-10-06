using System;
using System.Net;

namespace Core {
    public class UIRoot : UIContainer {

        public virtual void OnBackTo() {
            OnCome.Fire();
        }

        public virtual void OnBackFrom() {
            OnLeave.Fire();
        }

        public virtual void OnNavigateTo() {
            OnCome.Fire();
        }

        public virtual void OnNavigateFrom() {
            OnLeave.Fire();
        }

        public virtual void OnReactivate() { }

        public virtual void OnDeactivate() { }

        public virtual bool OnPressBack() {
            return true;
        }

        public EventNoArg OnLeave = new EventNoArg();
        public EventNoArg OnCome = new EventNoArg();
    }
}
