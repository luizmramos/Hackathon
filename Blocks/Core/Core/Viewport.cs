using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core {
    public class Viewport {
        public F2 viewportPositionOnScreen = new F2();
        public F2 viewportSizeOnScreen = new F2();

        public F2 viewportToScreenRatio = new F2();
        public float viewportYXRatio;
    
        private static F2 tmpF2 = new F2();
	
        public Viewport(F2 viewportPositionOnScreen, F2 viewportSizeOnScreen) {
	        this.viewportPositionOnScreen.Copy(viewportPositionOnScreen);
	        this.viewportSizeOnScreen.Copy(viewportSizeOnScreen);

	        viewportYXRatio = viewportSizeOnScreen.y / viewportSizeOnScreen.x;

	        viewportToScreenRatio.x = viewportSizeOnScreen.x;
	        viewportToScreenRatio.y = viewportSizeOnScreen.y / viewportYXRatio;
        }

        public void ViewportToScreen(F2 input, ref F2 output) {
	        output.x = input.x * viewportToScreenRatio.x + viewportPositionOnScreen.x;
            output.y = input.y * viewportToScreenRatio.y + viewportPositionOnScreen.y;
        }

        public void ScreenToViewport(F2 input, ref F2 output) {
	        output.x = (input.x - viewportPositionOnScreen.x) / viewportToScreenRatio.x;
            output.y = (input.y - viewportPositionOnScreen.y) / viewportToScreenRatio.y;
        }

        public void ViewportXToScreenXRatio(float input, out float output) {
	        output = input * viewportToScreenRatio.x;
        }

        public void ViewportToScreenRatio(F2 input, ref  F2 output) {
	        output.x = input.x * viewportToScreenRatio.x;
	        output.y = input.y * viewportToScreenRatio.y;
        }

        public void ScreenXToViewportXRatio(float input, out float output) {
	        output = input / viewportToScreenRatio.x;
        }

        public bool IsInsideViewport(F2 input) {
	        return input.x >= 0 && input.x <= 1 && input.y >= 0 && input.y <= viewportYXRatio;
        }

        public void ConvertPosition(Viewport other, F2 positionThis, ref  F2 positionOther) {
	        ViewportToScreen(positionThis, ref tmpF2);
	        other.ScreenToViewport(tmpF2, ref positionOther);
        }

    }
}
