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
    public class UISprite : UIElement {
        Texture2D texBackground;

        public F1 Rotation = new F1();
        public UISprite SetRotation(float value) { Rotation.Value = value; return this; }

        public Color Color = Color.White;
        public UISprite SetColor(int R, int G, int B) {
            Color.R = (byte)R; Color.G = (byte)G; Color.B = (byte)B;
            return this;
        }

        public UISprite(Texture2D tex) {
            Active = false;
            this.texBackground = tex;
        }

        public void SetTex(Texture2D tex) {
            this.texBackground = tex;
        }

        public override void Update(int dt) {

        }

        public override void Draw(int dt, float depth) {
            if (!Visible || texBackground == null) return;

            F2 screenPosVetor = new F2();
            //PositionManager.Get().UiToScreen(new F2(GetAbsoluteX(), GetAbsoluteY()), screenPosVetor);
            App.Get().defaultUIViewport.ViewportToScreen(new F2(GetAbsoluteX(), GetAbsoluteY()), ref screenPosVetor);
            F2 screenSizeVetor = new F2();
            //PositionManager.Get().UiToScreenRatio(Size, screenSizeVetor);
            App.Get().defaultUIViewport.ViewportToScreenRatio(Size, ref screenSizeVetor);
            screenSizeVetor.x /= texBackground.Width;
            screenSizeVetor.y /= texBackground.Height;

            App.Get().spriteBatchGui.Draw(texBackground, screenPosVetor.ToVector2(), null,
                Color.FromNonPremultiplied(Color.R, Color.G, Color.B, (int)(255 * GetAbsoluteOpacity())),
                Rotation.Value, new Vector2(texBackground.Width / 2, texBackground.Height / 2),
                screenSizeVetor.ToVector2(),
                SpriteEffects.None, depth);
        }

    }
}
