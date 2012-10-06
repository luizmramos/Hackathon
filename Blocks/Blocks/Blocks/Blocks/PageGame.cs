using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Blocks
{
    class PageGame : UIRoot
    {

        UIButton btnPlay;

        UILabel lblHello;

        UISprite imgStone;

        UIButton btnNext;

        AffectorF1 affStoneRotation;

        public PageGame()
        {

            Add(imgStone = new UISprite(Tex.stone),
                btnPlay = new UIButton(Tex.btnSimple, Tex.btnSimple_pressed));

            btnPlay.SetWidth(0.052f).SetHeight(0.052f).SetY(0.5f).SetX(0.5f);
        }
           

        public override void Update(int dt)
        {
            affStoneRotation.Update(dt);
            base.Update(dt);
        }

    }
}
