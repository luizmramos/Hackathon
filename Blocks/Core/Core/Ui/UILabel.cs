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
    public class UILabel : UIElement {
        SpriteFont font;

        private Color Color = Color.White;
        public UILabel SetColor(int R, int G, int B) {
            Color.R = (byte)R; Color.G = (byte)G; Color.B = (byte)B;
            return this;
        }

        public String Text;
        public UILabel SetText(String value) {
            Text = value;
            SetAlignment(alignment);
            return this;
        }

        public UILabel(SpriteFont font) {
            Text = "";
            Opacity.Value = 1;
            this.font = font;
        }

        Vector2 origin = Vector2.Zero;
        int alignment = 0;
        public UILabel SetAlignment(int alignment) {
            this.alignment = alignment;
            origin = font.MeasureString(Text);
            if (alignment == 0) {
                origin.X = 0;
            } else if (alignment == 1) {
                origin.X /= 2;
            }
            origin.Y /= 2;
            return this;
        }


        public bool Border = false;
        public UILabel SetBorder(bool value) { Border = value; return this; }

        Color borderColor = Color.FromNonPremultiplied(0, 0, 0, 255);

        public override void Draw(int dt, float depth) {
            if (!Visible) return;

            F2 screenPosVetor = new F2();
            //PositionManager.Get().UiToScreen(new F2(GetAbsoluteX(), GetAbsoluteY()),screenPosVetor);
            App.Get().defaultUIViewport.ViewportToScreen(new F2(GetAbsoluteX(), GetAbsoluteY()), ref screenPosVetor);

            Vector2 screenPos = screenPosVetor.ToVector2();

            float opacityFloat = GetAbsoluteOpacity();
            byte opacity = (byte)(255 * opacityFloat);
            Color color = Color.FromNonPremultiplied(Color.R, Color.G, Color.B, opacity);
            App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color,
                0, origin, 1, SpriteEffects.None, depth + 0.002f);
            if (Border) {
                color.R = borderColor.R;
                color.G = borderColor.G;
                color.B = borderColor.B;
                color.A = (byte)(255 * opacityFloat * opacityFloat * opacityFloat);

                screenPos.X -= 1;
                screenPos.Y -= 1;

                App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color, 0, origin, 1, SpriteEffects.None, depth + 0.001f);
                screenPos.X += 2;
                App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color, 0, origin, 1, SpriteEffects.None, depth + 0.001f);
                screenPos.Y += 2;
                App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color, 0, origin, 1, SpriteEffects.None, depth + 0.001f);
                screenPos.X -= 2;
                App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color, 0, origin, 1, SpriteEffects.None, depth + 0.001f);
                screenPos.X += 1;
                screenPos.Y += 1;
                App.Get().spriteBatchGui.DrawString(font, Text, screenPos, color, 0, origin, 1, SpriteEffects.None, depth + 0.001f);
            }
        }

    }
}
