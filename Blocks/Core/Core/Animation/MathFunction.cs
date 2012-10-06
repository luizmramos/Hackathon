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

namespace Core {
    public abstract class MathFunction {
        public float initialValue = 0;
        public float lastValue = 1;

        public abstract float GetValue(float x);
    }

    public class ExpFunction : MathFunction {
        public float b;
        public float exp;

        float den;
        public ExpFunction(float b, float exp) {
            this.b = b;
            this.exp = exp;
            den = (float)Math.Pow(b, exp) - 1;
        }

        public override float GetValue(float x) {
            float y = ((float)Math.Pow(b, x * exp) - 1) / den;

            return initialValue + y * (lastValue - initialValue);
        }

    }

    public class ExpTransFunction : MathFunction {
        public float b;
        public float exp;

        float den;
        public ExpTransFunction(float b, float exp) {
            this.b = b;
            this.exp = exp;
            den = (float)Math.Pow(b, exp) - 1;
        }

        public override float GetValue(float x) {
            float y = 1 - ((float)Math.Pow(b, (1 - x) * exp) - 1) / den;
            return initialValue + y * (lastValue - initialValue);
        }

    }
}
