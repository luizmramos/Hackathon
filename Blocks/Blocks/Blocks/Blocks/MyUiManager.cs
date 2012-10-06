using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class MyUiManager : UIManager
    {

        private static UIManager instance;
        public static MyUiManager Get()
        {
            return (MyUiManager)instance;
        }

        public static void Init()
        {
            instance = new MyUiManager();
        }

        PageMenu pageMenu;
        PageInst1 pageInst1;
        PageLevel pageLevel;
        PageFases pageFases;
        PageGame pageGame;

        public MyUiManager()
        {
            pageMenu = new PageMenu();
            Switch(pageMenu);
        }

        public void SwitchtoLevel()
        {
            pageLevel = new PageLevel();
            Switch(pageLevel);
        }

        public void SwitchtoInst1()
        {
            pageInst1 = new PageInst1();
            Switch(pageInst1);
        }

        public void SwitchtoMenu()
        {
            Switch(pageMenu);
        }

        public void SwitchtoFases(int level)
        {
            pageFases = new PageFases(level);
            Switch(pageFases);
        }

        public void SwitchtoGame(int level, int fase)
        {
            pageGame = new PageGame(level, fase);
            Switch(pageGame);
        }
    }
}
