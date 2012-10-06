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

    public enum TouchState { Moved, Pressed, Released };

    public class TouchInfo {
        public int id;
        public F2 Position = new F2();
        public F2 Delta = new F2();
        public TouchState TouchState;

        public void setDelta(F2 p1, F2 p2) {
            Delta.x = p2.x - p1.y;
            Delta.y = p2.y - p1.y;
        }

        public void Set(float x, float y, TouchState touchState) {
            Position.x = x;
            Position.y = y;
            this.TouchState = touchState;
        }

        public void Copy(TouchInfo ti) {
            id = ti.id;
            Position.Copy(ti.Position);
            TouchState = ti.TouchState;
        }
    }

    public class UIManager {
        protected Stack<UIRoot> navigationStack = new Stack<UIRoot>();

        private UIOverlay overlay;

        private UIRoot CurrentPage;

        //protected static GamePage game;

        Board frontBoard;
        AffectorSet affSwitchRoot;
        AffectorF1 affFade;
        bool blockInput = false;

        UILabel lblPos;

        public UIManager() {
            frontBoard = new Board() { Color = Color.Black };
            affSwitchRoot = new AffectorSet()
                .Add(affFade = new AffectorF1(frontBoard.Opacity, 0, 1, 50), 0)
                .Add(new AffectorF1(frontBoard.Opacity, 1, 0, 100), 50);

            lblPos = new UILabel(App.Get().defaultFont);
            lblPos.SetX(10f / 480).SetY(600f / 480);

            CreateHandlers();
        }

        public bool OverlayShown() {
            return overlay != null;
        }

        protected void SetOverlay(UIOverlay message) {
            this.overlay = message;
            message.OnNavigateTo();
            frontBoard.Opacity.Value = 0.5f;
        }

        public void HideOverlay(int option) {
            UIOverlay prevOverlay = this.overlay;
            this.overlay = null;
            prevOverlay.OnNavigateFrom();
            prevOverlay.GiveAnswer(option);
            frontBoard.Opacity.Value = 0;
        }

        private void CreateHandlers() {
            affSwitchRoot.End.Add(affSwitchRoot_End);
            affFade.End.Add(affFade_End);
        }

        void affFade_End() {
            CurrentPage = nextPage;
            nextPage.OnNavigateTo();
        }

        void affSwitchRoot_End() {
            blockInput = false;
        }

        public void PressBack() {
            if (overlay != null) {
                if (overlay.OnPressBack()) GoBack();
            } else {
                if (CurrentPage.OnPressBack()) GoBack();
            }
        }

        public void GoBack() {
            if (overlay != null) {
                overlay.OnBackFrom();
                return;
            }

            CurrentPage.OnBackFrom();

            if (navigationStack.Count == 0) {
                //UserSense.QuitGame(); TODO
                App.Get().Exit();
                return;
            };

            CurrentPage = navigationStack.Pop();
            CurrentPage.OnBackTo();
        }

        UIRoot nextPage;
        protected virtual void Switch(UIRoot nextPage) {
            if (CurrentPage != null) {
                navigationStack.Push(CurrentPage);
                CurrentPage.OnNavigateFrom();
                blockInput = true;
                this.nextPage = nextPage;
                affSwitchRoot.Begin();
            } else {
                CurrentPage = nextPage;
                nextPage.OnNavigateTo();
            }
        }

        F2 lastPressPos = new F2();
        UIElement draggedObject;
        public virtual bool Consume(TouchInfo touchInfo) {
            if (blockInput) return true;

            UIElement toConsume = null;
            if (overlay != null) {
                toConsume = overlay.GetConsumedElement(touchInfo);
            } else {
                toConsume = CurrentPage.GetConsumedElement(touchInfo);
            }

            if (touchInfo.TouchState == TouchState.Released) {
                touchInfo.Delta = touchInfo.Position - lastPressPos;
                if (draggedObject != null) {
                    toConsume = draggedObject;
                    draggedObject = null;
                }
            } else if (touchInfo.TouchState == TouchState.Pressed) {
                if (draggedObject == null) {
                    draggedObject = toConsume;
                    lastPressPos.Copy(touchInfo.Position);
                }
            } else if (touchInfo.TouchState == TouchState.Moved) {
                if (draggedObject != null) {
                    toConsume = draggedObject;
                }
                touchInfo.Delta = touchInfo.Position - lastPressPos;
            }

            if (toConsume == null) return overlay != null;
            toConsume.Consume(touchInfo);
            return true;
        }

        public void Reactivate() {
            if (CurrentPage != null) CurrentPage.OnReactivate();
        }

        public void Deactivate() {
            if (CurrentPage != null) CurrentPage.OnDeactivate();
        }

        public void Update(int dt) {
            affSwitchRoot.Update(dt);
            if (overlay != null) overlay.Update(dt);
            CurrentPage.Update(dt);
        }


        public void Draw(int dt) {
            if (blockInput) frontBoard.Draw(0.999f);
#if DEBUG_POS
            lblPos.Draw(dt, 0.99f);
#endif
            if (overlay != null) {
                frontBoard.Draw(0.95f);
                overlay.Draw(dt, 0.99f);
            }

            CurrentPage.Draw(dt, 0);
        }
    }

    class Board {
        Texture2D texPixel;
        public Color Color;

        public F1 Opacity = new F1();

        public Board() {
            texPixel = App.Get().Content.Load<Texture2D>("pixel");
        }

        public void Draw(float depth) {
            App.Get().spriteBatchGui.Draw(texPixel, new Rectangle(0, 0, 480, 800), null,
                Color.FromNonPremultiplied(Color.R, Color.G, Color.B, (int)(255 * Opacity.Value)),
                0, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}
