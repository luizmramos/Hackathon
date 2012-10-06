using System;
using System.Net;

namespace Core {
    public class UIPivot : UIContainer {

        int index;
        int maxIndex = 0;

        public int GetIndex() {
            return index;
        }

        AffectorF2 affSlide;

        float slotsWidth;

        UIContainer slideContainer;

        public UIPivot(float slotsWidth) {

            this.slotsWidth = slotsWidth;

            slideContainer = new UIContainer();
            affSlide = new AffectorF2(slideContainer.Position).SetFunc(new ExpTransFunction(2, 6));

            InternAdd(slideContainer);

            affSlide.End.Add(affSlide_End);
        }

        bool moveAgain = false;
        void affSlide_End() {
            if (!moveAgain) {
                OnSlideEnd.Fire(index);
                return;
            }

            affSlide.SetValues(slideContainer.Position, new F2(NextPos(index), slideContainer.Position.y), 500);
            affSlide.Begin();
            moveAgain = false;
        }


        UIElement lastConsumed;
        public override UIElement GetConsumedElement(TouchInfo ti) {
            if (!Active) return null;

            lastConsumed = base.GetConsumedElement(ti);
            if (lastConsumed != null || holding) return this;
            return lastConsumed;
        }

        float holdPos = 0;
        bool holding = false;
        float maxDelta = 0;
        public override void Consume(TouchInfo ti) {
            if (ti.TouchState == TouchState.Released) {
                if (ti.Delta.x > 25f / 480) {
                    SlidePrev();
                } else if (ti.Delta.x < -25f / 480) {
                    SlideNext();
                } else {
                    if (Math.Abs(maxDelta) < 25f / 480)
                        if (lastConsumed != null) lastConsumed.Consume(ti);
                }
                holding = false;
                maxDelta = 0;
            } else if (ti.TouchState == TouchState.Moved) {
                if (Math.Abs(ti.Delta.x) > Math.Abs(maxDelta)) maxDelta = ti.Delta.x;
                slideContainer.Position.x = ti.Delta.x + holdPos;
            } else if (ti.TouchState == TouchState.Pressed) {
                holding = true;
                holdPos = slideContainer.Position.x;
                maxDelta = 0;
            } else {
                if (lastConsumed != null) lastConsumed.Consume(ti);
            }
        }

        public override void Update(int dt) {
            if (!affSlide.Active && !holding) {
                float d = NextPos(index) - slideContainer.Position.x;
                if (d > 3f / 480 || d < -3f / 480)
                    affSlide.SetValues(NextPos(index), slideContainer.Position.y, 400);

            }
            if (!holding)
                affSlide.Update(dt);

            base.Update(dt);
        }

        public void SlideNext() {
            Slide(index + 1);
        }

        public void SlidePrev() {
            Slide(index - 1);
        }

        private void Slide(int index) {
            if (index < 0) {
                index = 0;
            } else if (index >= maxIndex) {

                index = maxIndex - 1;
            }

            this.index = index;
            affSlide.SetValues(NextPos(index), slideContainer.Position.y, 400);
            affSlide.Begin();
            OnSlideEvent.Fire(index);
        }

        private float NextPos(int index) {
            return Position.x - index * slotsWidth;
        }

        private void InternAdd(UIElement elem) {
            base.Add(elem);
        }

        public override UIContainer Add(UIElement elem) {
            UIContainer container = new UIContainer();
            container.SetWidth(slotsWidth).SetX(slideContainer.Count() * slotsWidth);
            slideContainer.Add(container);
            container.Add(elem);

            this.maxIndex++;
            return this;
        }

        public override UIContainer Add(params UIElement[] elems) {
            foreach (UIElement elem in elems) {
                UIContainer container = new UIContainer();
                container.SetWidth(slotsWidth).SetX(slideContainer.Count() * slotsWidth);
                slideContainer.Add(container);
                container.Add(elem);
                this.maxIndex++;
            }
            return this;
        }

        public EventOneArg<int> OnSlideEvent = new EventOneArg<int>();
        public EventOneArg<int> OnSlideEnd = new EventOneArg<int>();
    }
}
