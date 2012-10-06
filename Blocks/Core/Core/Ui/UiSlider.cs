using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Core {
    public class UiSlider : UIElement {
        public Color Color = Color.Green;
        public Color CursorColor = Color.White;

        public float Value = 0;

        Texture2D texBar;
        Texture2D texCursor;

        public UiSlider() {
            texBar = App.Get().Content.Load<Texture2D>("gui_volume");
            texCursor = App.Get().Content.Load<Texture2D>("gui_cursor");

            OnMoved.Add(Slider_OnMoved);
            Size.y = 100f / 480;
        }

        public EventOneArg<float> OnChangeValue = new EventOneArg<float>();

        void Slider_OnMoved(TouchInfo ti) {
            Value = (ti.Position.x - GetAbsoluteX()) / Size.x + 0.5f;
            if (Value < 0) Value = 0;
            else if (Value > 1) Value = 1;
            OnChangeValue.Fire(Value);
        }

        const float barHeight = 10f / 480;
        const float cursorSide = 18f / 480;

        F2 screenPos = new F2();
        F2 cursorPos = new F2();
        public override void Draw(int dt, float depth) {

            float screenBarHeight = 0;
            //PositionManager.Get().UiXToScreenXRatio(barHeight, ref screenBarHeight);
            App.Get().defaultUIViewport.ViewportXToScreenXRatio(barHeight, out screenBarHeight);
            float screenBarWidth = 0;
            //PositionManager.Get().UiXToScreenXRatio(Size.x, ref screenBarWidth);
            App.Get().defaultUIViewport.ViewportXToScreenXRatio(barHeight, out screenBarHeight);

            F2 absolutePos = new F2(GetAbsoluteX(), GetAbsoluteY());
            //PositionManager.Get().UiToScreen(absolutePos, screenPos);
            App.Get().defaultUIViewport.ViewportToScreen(absolutePos, ref screenPos);

            cursorPos.y = absolutePos.y;
            cursorPos.x = absolutePos.x + (Value - 0.5f) * Size.x;

            //PositionManager.Get().UiToScreen(cursorPos, cursorPos);
            App.Get().defaultUIViewport.ViewportToScreen(cursorPos, ref cursorPos);

            float screenCursorSide = 0;
            //PositionManager.Get().UiXToScreenXRatio(cursorSide, ref screenCursorSide);
            App.Get().defaultUIViewport.ViewportXToScreenXRatio(cursorSide, out screenCursorSide);

            int opacity = (int)(255 * GetAbsoluteOpacity());
            float absoluteDepth = GetAbsoluteDepth();

            App.Get().spriteBatchGui.Draw(texBar,
                screenPos.ToVector2(),
                null,
                Color.FromNonPremultiplied(Color.R, Color.G, Color.B, opacity),
                0, new Vector2(texBar.Width / 2, texBar.Height / 2),
                new Vector2(screenBarWidth / texBar.Width, screenBarHeight / texBar.Height),
                SpriteEffects.None, absoluteDepth);
            App.Get().spriteBatchGui.Draw(texCursor,
                cursorPos.ToVector2(),
                null,
                Color.FromNonPremultiplied(CursorColor.R, CursorColor.G, CursorColor.B, opacity),
                0, new Vector2(texCursor.Width / 2, texCursor.Width / 2),
                new Vector2(screenCursorSide / texCursor.Width, screenCursorSide / texCursor.Height),
                SpriteEffects.None, absoluteDepth + 0.01f);
        }
    }
}
