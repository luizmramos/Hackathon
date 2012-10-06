using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Blocks {

    class Tex {

        public static Texture2D pixel;
        public static Texture2D btnSimple;
        public static Texture2D btnSimple_pressed;
        public static Texture2D stone;
        
        public static void Init() {
            pixel = MyApp.Get().Content.Load<Texture2D>("pixel");
            btnSimple = MyApp.Get().Content.Load<Texture2D>("btnSimple");
            btnSimple_pressed = MyApp.Get().Content.Load<Texture2D>("btnSimple_pressed");
            stone = MyApp.Get().Content.Load<Texture2D>("stone");
        }
    }
}
