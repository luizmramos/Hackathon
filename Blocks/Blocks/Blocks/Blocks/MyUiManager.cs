using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks {
    class MyUiManager : UIManager {

        private static UIManager instance;
        public static MyUiManager Get() {
            return (MyUiManager)instance;
        }

        public static void Init() {
            instance = new MyUiManager();
        }

        PageMenu pageMenu;
        PageInst1 pageInst1;
        PageLevel pageLevel;
        PageFases pageFases;
        PageGame pageGame;

        public MyUiManager() {
            pageMenu = new PageMenu();
            pageInst1 = new PageInst1();
            pageLevel = new PageLevel();
            pageFases = new PageFases();
            pageGame = new PageGame();
            Switch(pageMenu);
        }
    }
}
