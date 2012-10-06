using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace Core {
    public class UIContainer : UIElement {

        protected List<UIElement> uielements;

        public int Count() {
            return uielements.Count;
        }

        public UIContainer() {
            uielements = new List<UIElement>();
        }

        public override UIElement GetConsumedElement(TouchInfo ti) {
            UIElement toConsume = null;
            if (!Visible) return null;
            if (!Active) return null;

            foreach (UIElement element in uielements) {
                UIElement possibleConsumed = element.GetConsumedElement(ti);
                if (toConsume == null)
                    toConsume = possibleConsumed;
                else if (possibleConsumed != null) {
                    if (toConsume.GetAbsoluteDepth() < possibleConsumed.GetAbsoluteDepth())
                        toConsume = possibleConsumed;
                }
            }
            return toConsume;
        }

        public override void Update(int dt) {
            foreach (UIElement element in uielements) {
                element.Update(dt);
            }
        }

        public override void Draw(int dt, float depth) {
            if (!Visible) return;

            foreach (UIElement element in uielements) {
                element.Draw(dt, depth + element.Depth / 10);
            }
        }

        public virtual UIContainer Add(UIElement elem) {
            uielements.Add(elem);
            elem.Parent = this;
            return this;
        }

        public virtual UIContainer Add(params UIElement[] elems) {
            foreach (UIElement elem in elems) {
                uielements.Add(elem);
                elem.Parent = this;
            }
            return this;
        }

    }
}
