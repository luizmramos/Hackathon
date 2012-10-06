using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Core {
    public abstract class UIElement : IDraw, IUpdate {
        public UIContainer Parent;

        public F2 Position = new F2();

        public UIElement SetX(float x) { Position.x = x; return this; }
        public float GetX(float x) { return Position.x; }

        public UIElement SetY(float y) { Position.y = y; return this; }
        public float GetY(float y) { return Position.y; }


        public UIElement SetUiClass(UIClass value) { value.Apply(this); return this; }

        public F1 Opacity = new F1();
        public UIElement SetOpacity(float value) { Opacity.Value = value; return this; }
        public float GetAbsoluteOpacity() {
            if (Parent == null)
                return Opacity.Value;
            else
                return Parent.GetAbsoluteOpacity() * Opacity.Value;
        }

        public float GetAbsoluteX() {
            if (Parent == null)
                return Position.x;
            else
                return Parent.GetAbsoluteX() + Position.x;
        }

        public float GetAbsoluteY() {
            if (Parent == null)
                return Position.y;
            else
                return Parent.GetAbsoluteY() + Position.y;
        }

        public float GetAbsoluteDepth() {


            if (Parent == null)
                return Depth;
            else
                return Parent.GetAbsoluteDepth() + Depth / 10;
        }

        public float Depth;
        public UIElement SetDepth(float value) { Depth = value; return this; }

        public F2 Size = new F2();
        public float GetWidth() { return Size.x; }
        public float GetHeight() { return Size.y; }
        public UIElement SetWidth(float value) { Size.x = value; return this; }
        public UIElement SetHeight(float value) { Size.y = value; return this; }

        protected bool pressed = false;
        protected bool mouseOver = false;

        public bool Visible;
        public UIElement SetVisible(bool value) { Visible = value; return this; }

        public bool Active;
        public UIElement SetActive(bool value) { Active = value; return this; }

        public UIElement() {
            Visible = true;
            Active = true;
            Opacity.Value = 1;
        }

        public virtual UIElement GetConsumedElement(TouchInfo ti) {
            if (!Visible) return null;
            if (!Active) return null;

            bool canConsume = CheckBounds(ti);
            mouseOver = canConsume && ti.TouchState != TouchState.Released;
            pressed = (ti.TouchState == TouchState.Pressed || pressed) && mouseOver;

            if (!canConsume) return null;
            return this;
        }

        public virtual void Consume(TouchInfo ti) {
            if (ti.TouchState == TouchState.Pressed) {
                mouseOver = true;
                if (OnPressed != null)
                    OnPressed.Fire(ti);
            } else if (ti.TouchState == TouchState.Released) {
                mouseOver = false;
                if (ti.Delta.Length2() < 2500f / 480 / 480)
                    if (OnRelease != null)
                        OnRelease.Fire(ti);
            } else if (ti.TouchState == TouchState.Moved) {
                mouseOver = true;
                OnMoved.Fire(ti);
            }
        }

        protected void FireOnRelease(TouchInfo ti) {
            OnRelease.Fire(ti);
        }

        public EventOneArg<TouchInfo> OnRelease = new EventOneArg<TouchInfo>();
        public EventOneArg<TouchInfo> OnPressed = new EventOneArg<TouchInfo>();
        public EventOneArg<TouchInfo> OnMoved = new EventOneArg<TouchInfo>();

        public float ExtensionX;
        public UIElement SetExtensionX(float value) { ExtensionX = value; return this; }

        public float ExtensionY;
        public UIElement SetExtensionY(float value) { ExtensionY = value; return this; }

        protected bool CheckBounds(TouchInfo ti) {
            float x = GetAbsoluteX();
            float y = GetAbsoluteY();
            float hW = Size.x / 2 + ExtensionX;
            float hH = Size.y / 2 + ExtensionY;

            return ti.Position.x >= x - hW && ti.Position.x <= x + hW &&
                ti.Position.y >= y - hH && ti.Position.y <= y + hH;
        }

        public virtual void Update(int dt) {
        }

        public void Draw(int dt) {
            Draw(dt, 0.9f);
        }

        public abstract void Draw(int dt, float depth);
    }
}
