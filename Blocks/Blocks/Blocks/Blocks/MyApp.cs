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
using Core;

namespace Blocks {
    public class MyApp : App {
        GraphicsDeviceManager graphics;

        public MyApp() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            InitScreenSize(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            graphics.IsFullScreen = true;

            defaultUIViewport = new Core.Viewport(F2.Zero(), screenSize);
            defaultGameViewport = new Core.Viewport(F2.Zero(), screenSize);

            TargetElapsedTime = TimeSpan.FromTicks(166666);

            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize() {
            App.AppStart(this);

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchGui = new SpriteBatch(GraphicsDevice);
            spriteBatchAdd = new SpriteBatch(GraphicsDevice);

            Tex.Init();
            Font.Init();
            defaultFont = Font.andy30;

            MyUiManager.Init();
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        TouchInfo touchInfo = new TouchInfo();
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                MyUiManager.Get().PressBack();

            int dt = gameTime.ElapsedGameTime.Milliseconds;

            TouchCollection tc = TouchPanel.GetState();
            foreach(TouchLocation tl in tc){
                ToTouchInfo(tl, ref touchInfo);
                App.Get().defaultUIViewport.ScreenToViewport(touchInfo.Position, ref touchInfo.Position);
                MyUiManager.Get().Consume(touchInfo);
            }

            MyUiManager.Get().Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int dt = gameTime.ElapsedGameTime.Milliseconds;
            spriteBatchAdd.Begin();
            spriteBatchGui.Begin();
            spriteBatch.Begin();
            MyUiManager.Get().Draw(dt);
            spriteBatch.End();
            spriteBatchGui.End();
            spriteBatchAdd.End();
            
            base.Draw(gameTime);
        }
    }
}
