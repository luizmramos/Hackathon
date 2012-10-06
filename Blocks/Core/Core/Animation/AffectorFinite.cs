using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public abstract class AffectorFinite : IUpdate {

        public bool Active = false;

        public float deltaT;
        protected float currentTime;

        protected float GetFraction() {
            return (((float)currentTime) / deltaT);
        }

        public virtual void Update(int dt) {
            currentTime += dt;
            if (currentTime >= deltaT) {
                currentTime = deltaT;
                Active = false;
                FireEnd();
            }
        }

        public virtual void Begin() {
            currentTime = 0;
            Active = true;
        }
        public virtual AffectorFinite Stop() {
            Active = false;
            return this;
        }

        public EventNoArg End = new EventNoArg();

        protected bool looped;
        public AffectorFinite SetLooped(bool loop) {
            this.looped = loop;
            return this;
        }

        void AffectorFinite_End() {
            Begin();
        }

        protected void FireEnd() {
            End.Fire();

            if (looped) {
                Begin();
            }
        }

        public abstract void SetEndParameters();
    }

    public class AffectorF2 : AffectorFinite {
        F2 obj;

        F2 p1 = new F2();
        F2 p2 = new F2();
        float curvature;

        F2 perp = new F2();

        public MathFunction func = null;

        protected bool startFromBegining = false;

        public AffectorF2(F2 obj) {
            this.obj = obj;
        }

        public AffectorF2(F2 obj, F2 p1, F2 p2, float deltaT) {
            this.obj = obj;
            SetValues(p1, p2, deltaT);
        }

        public AffectorF2(F2 obj, F2 p2, float deltaT) {
            this.obj = obj;
            SetValues(p2, deltaT);
        }

        public AffectorF2(F2 obj, float x1, float y1, float x2, float y2, float deltaT) {
            this.obj = obj;
            SetValues(x1, y1, x2, y2, deltaT);
        }

        public AffectorF2(F2 obj, float x2, float y2, float deltaT) {
            this.obj = obj;
            SetValues(x2, y2, deltaT);
        }

        public void SetValues(F2 p1, F2 p2, float deltaT) {
            startFromBegining = false;
            this.p1.Copy(p1);
            this.p2.Copy(p2);

            this.deltaT = deltaT;
        }

        public void SetValues(F2 p2, float deltaT) {
            startFromBegining = true;
            this.p2.Copy(p2);

            this.deltaT = deltaT;
        }

        public void SetValues(float x1, float y1, float x2, float y2, float deltaT) {
            startFromBegining = false;
            this.p1.x = x1;
            this.p1.y = y1;
            this.p2.x = x2;
            this.p2.y = y2;
            this.deltaT = deltaT;
        }

        public void SetValues(float x2, float y2, float deltaT) {
            startFromBegining = true;
            this.p2.x = x2;
            this.p2.y = y2;
            this.deltaT = deltaT;
        }

        public AffectorF2 SetCurv(float curvature) {
            this.curvature = curvature;
            CalculatePerp();
            return this;
        }

        public AffectorF2 SetFunc(MathFunction func) {
            this.func = func;
            return this;
        }

        private void CalculatePerp() {
            F2 diff = p2 - p1;
            if (curvature != 0) {
                perp.y = diff.x * curvature;
                perp.x = -diff.y * curvature;
            }
        }

        public override void Begin() {
            if (startFromBegining) {
                p1.x = obj.x;
                p1.y = obj.y;
            } else {
                obj.x = p1.x;
                obj.y = p1.y;
            }


            base.Begin();
        }

        float parabolic = 0;
        float fraction = 0;
        public override void Update(int dt) {
            if (!Active) return;

            base.Update(dt);

            fraction = GetFraction();
            if (func != null) {
                fraction = func.GetValue(fraction);
            }

            if (curvature == 0) {
                obj.x = p1.x + ((p2.x - p1.x) * fraction);
                obj.y = p1.y + ((p2.y - p1.y) * fraction);
            } else {
                parabolic = GetParabolicBias(fraction);

                obj.x = p1.x + ((p2.x - p1.x) * fraction) + (perp.x * parabolic);
                obj.y = p1.y + ((p2.y - p1.y) * fraction) + (perp.y * parabolic);
            }
        }

        private float GetParabolicBias(float fraction) {
            return 4.0f * (0.25f - ((fraction - 0.5f) * (fraction - 0.5f)));
        }

        public override void SetEndParameters() {
            obj.x = p2.x;
            obj.y = p2.y;
        }
    }


    public class AffectorF1 : AffectorFinite {
        F1 obj;

        float r1;
        float r2;

        MathFunction func = null;

        bool startFromBegining = false;

        public void SetTarget(F1 obj) {
            this.obj = obj;
        }

        public AffectorF1(F1 obj) {
            this.obj = obj;
        }

        public AffectorF1(F1 obj, float r1, float r2, float deltaT) {
            this.obj = obj;
            SetValues(r1, r2, deltaT);
        }

        public AffectorF1(F1 obj, float r2, float deltaT) {
            this.obj = obj;
            SetValues(r2, deltaT);
        }

        public void SetValues(float r1, float r2, float deltaT) {
            startFromBegining = false;
            this.r1 = r1;
            this.r2 = r2;
            this.deltaT = deltaT;
        }

        public void SetValues(float r2, float deltaT) {
            startFromBegining = true;
            this.r2 = r2;
            this.deltaT = deltaT;
        }

        public AffectorF1 SetFunc(MathFunction func) {
            this.func = func;
            return this;
        }

        public override void Begin() {
            if (startFromBegining) {
                r1 = obj.Value;
            } else {
                obj.Value = r1;
            }

            base.Begin();
        }

        public override void Update(int dt) {
            if (!Active) return;

            base.Update(dt);

            if (func == null) {
                obj.Value = r1 + (r2 - r1) * GetFraction();
            } else {
                obj.Value = r1 + (r2 - r1) * func.GetValue(GetFraction());
            }
        }

        public override void SetEndParameters() {
            obj.Value = r2;
        }
    }

    public class AffectorWait : AffectorFinite {

        public AffectorWait(float deltaT) {
            this.deltaT = deltaT;
        }

        public override void Update(int dt) {
            if (!Active) return;

            base.Update(dt);
        }

        public override void SetEndParameters() { /* Do nothing :\ */}
    }
}
