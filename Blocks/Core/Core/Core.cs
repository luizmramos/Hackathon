using System;
using System.Net;
using Microsoft.Xna.Framework;

namespace Core {
    public class F2 {
        public float x;
        public float y;

        public F2() {

        }

        public F2(F2 toCopy) {
            this.x = toCopy.x;
            this.y = toCopy.y;
        }

        public F2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToVector2() {
            return new Vector2(x, y);
        }

        public void Normalize() {
            float l = Length();
            x /= l;
            y /= l;
        }

        public void Copy(F2 copiedVetor) {
            this.x = copiedVetor.x;
            this.y = copiedVetor.y;
        }

        public float Length() {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public float Length2() {
            return x * x + y * y;
        }

        public static F2 Zero() {
            return new F2(0, 0);
        }

        public static F2 operator +(F2 v1, F2 v2) {
            return new F2(v1.x + v2.x, v1.y + v2.y);
        }

        public static F2 operator *(F2 v1, F2 v2) {
            return new F2(v1.x * v2.x, v1.y * v2.y);
        }

        public static F2 operator -(F2 v1, F2 v2) {
            return new F2(v1.x - v2.x, v1.y - v2.y);
        }

        public static F2 operator *(F2 v1, float f) {
            return new F2(v1.x * f, v1.y * f);
        }

        public static F2 operator *(float f, F2 v1) {
            return new F2(v1.x * f, v1.y * f);
        }

        public static F2 operator /(F2 v1, float f) {
            return new F2(v1.x / f, v1.y / f);
        }
    }

    public class F1 {
        public float Value;
    }

    public interface IDraw {
        void Draw(int dt);
    }
    public interface IUpdate {
        void Update(int dt);
    }
}
