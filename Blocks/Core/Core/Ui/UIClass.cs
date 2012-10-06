using System;
using System.Net;

namespace Core {
    public class UIClass {

        const float defaultValue = -10000000;

        public F2 Position = new F2();
        public F2 Size = new F2();
        public F1 Opacity = new F1();
        public float Depth;

        public UIClass SetX(float value) { Position.x = value; return this; }
        public UIClass SetY(float value) { Position.y = value; return this; }
        public UIClass SetWidth(float value) { Size.x = value; return this; }
        public UIClass SetHeight(float value) { Size.y = value; return this; }
        public UIClass SetOpacity(float value) { Opacity.Value = value; return this; }
        public UIClass SetDepth(float value) { Depth = value; return this; }

        public UIClass() {
            Position.x = defaultValue;
            Position.y = defaultValue;
            Size.x = defaultValue;
            Size.y = defaultValue;
            Opacity.Value = defaultValue;
            Depth = defaultValue;
        }

        public void Apply(UIElement elem) {
            if (Position.x != defaultValue)
                elem.SetX(Position.x);
            if (Position.y != defaultValue)
                elem.SetY(Position.y);
            if (Size.x != defaultValue)
                elem.SetWidth(Size.x);
            if (Size.y != defaultValue)
                elem.SetHeight(Size.y);
            if (Opacity.Value != defaultValue)
                elem.SetOpacity(Opacity.Value);
            if (Depth != defaultValue)
                elem.Depth = Depth;
        }
    }
}
