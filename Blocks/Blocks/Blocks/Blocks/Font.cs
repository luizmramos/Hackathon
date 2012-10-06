using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

using Core;

namespace Blocks {
    class Font {
        public static SpriteFont andy30;

        public static void Init() {
            andy30 = App.Get().Content.Load<SpriteFont>("andy30");
        }
    }
}
