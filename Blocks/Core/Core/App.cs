using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Core {
    public abstract class App : Microsoft.Xna.Framework.Game {

        public Viewport defaultUIViewport;
        public Viewport defaultGameViewport;

        public SpriteFont defaultFont;

        public F2 screenSize;
        public float screenYXRatio;

        protected virtual void InitScreenSize(float screenWidth, float screenHeight) {
            screenSize = new F2(screenWidth, screenHeight);
            screenYXRatio = screenHeight / screenWidth;
        }

        protected static App app;
        public static App Get() {
            return app;    
        }

        public SpriteBatch spriteBatch;
        public SpriteBatch spriteBatchGui;
        public SpriteBatch spriteBatchAdd;

        public Random Rand = new Random();

        private bool ZuneActive() {
            return !MediaPlayer.GameHasControl;
        }

        public static void AppStart(App app) {
            if (App.app != null) {
                throw new Exception("App has been started twice");
            }
            App.app = app;
        }

        public void ToTouchInfo(TouchLocation tl, ref TouchInfo ti) {
            TouchState touchState = TouchState.Moved;
            if (tl.State == TouchLocationState.Pressed)
                touchState = TouchState.Pressed;
            else if (tl.State == TouchLocationState.Released)
                touchState = TouchState.Released;

            ti.id = tl.Id;
            ti.Set(tl.Position.X, tl.Position.Y, touchState);
        }
    }
}