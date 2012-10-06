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
    public class UIButton : UIContainer {
        UISprite sprite;
        UISprite spritePressed;

        public UIButton(Texture2D tex, Texture2D texPressed) {
            Add(
                sprite = new UISprite(tex),
                spritePressed = new UISprite(texPressed)
            );

            sprite.Active = true;
            spritePressed.Active = true;
            SetTexture(tex, texPressed);
        }

        public UIButton(Texture2D tex) {
            Add(
                sprite = new UISprite(tex),
                spritePressed = new UISprite(tex)
            );
            sprite.Active = true;
            spritePressed.Active = true;
            SetTexture(tex);
        }

        bool hasPressedTexture = false;
        public void SetTexture(Texture2D tex, Texture2D texPressed) {
            sprite.SetTex(tex);
            if (texPressed == null) {
                hasPressedTexture = false;
            } else {
                hasPressedTexture = true;
                spritePressed.SetTex(texPressed);
            }
        }

        public void SetTexture(Texture2D tex) {
            hasPressedTexture = false;
            sprite.SetTex(tex);
        }

        public override UIElement GetConsumedElement(TouchInfo ti) {
            if (!Visible) return null;
            if (!Active) return null;

            bool canConsume = CheckBounds(ti);
            mouseOver = canConsume && ti.TouchState != TouchState.Released;
            pressed = (ti.TouchState == TouchState.Pressed || pressed) && mouseOver;

            if (!canConsume) return null;
            return this;
        }

        public override void Draw(int dt, float depth) {
            if (!Visible) return;

            if (pressed && hasPressedTexture) {
                Sync(spritePressed);
                spritePressed.Draw(dt, depth);
            } else {
                Sync(sprite);
                sprite.Draw(dt, depth);
            }
        }

        protected void Sync(UIElement sprite) {
            sprite.Size.Copy(Size);
        }

    }
}
